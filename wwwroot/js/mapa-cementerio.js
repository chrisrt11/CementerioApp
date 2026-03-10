// mapa-cementerio.js - Lógica del mapa interactivo con Leaflet.js

let mapa;
let marcadores = [];

// Iconos personalizados
const iconoOcupado = L.divIcon({
    className: '',
    html: '<div style="background-color:#dc3545;width:16px;height:16px;border-radius:50%;border:2px solid #fff;box-shadow:0 0 4px rgba(0,0,0,0.5);"></div>',
    iconSize: [16, 16],
    iconAnchor: [8, 8]
});

const iconoDisponible = L.divIcon({
    className: '',
    html: '<div style="background-color:#198754;width:16px;height:16px;border-radius:50%;border:2px solid #fff;box-shadow:0 0 4px rgba(0,0,0,0.5);"></div>',
    iconSize: [16, 16],
    iconAnchor: [8, 8]
});

// Inicializar mapa
function inicializarMapa() {
    mapa = L.map('mapa').setView([-34.6037, -58.3816], 16);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors',
        maxZoom: 19
    }).addTo(mapa);

    cargarLotes();
}

// Cargar todos los lotes en el mapa
function cargarLotes(sector, estado) {
    let url = '/api/lotes/filtrar';
    const params = [];
    if (sector) params.push(`sector=${encodeURIComponent(sector)}`);
    if (estado) params.push(`estado=${encodeURIComponent(estado)}`);
    if (params.length > 0) url += '?' + params.join('&');

    fetch(url)
        .then(response => response.json())
        .then(lotes => {
            limpiarMarcadores();
            lotes.forEach(lote => agregarMarcador(lote));
            mostrarNotificacion(`Se cargaron ${lotes.length} lote(s)`);
        })
        .catch(error => {
            console.error('Error al cargar lotes:', error);
            mostrarNotificacion('Error al cargar los lotes', 'error');
        });
}

// Agregar un marcador al mapa
function agregarMarcador(lote) {
    const icono = lote.estado === 'Ocupado' ? iconoOcupado : iconoDisponible;

    const popupContent = `
        <div class="popup-lote">
            <strong>${lote.identificador}</strong><br/>
            <span class="badge ${lote.estado === 'Ocupado' ? 'bg-danger' : 'bg-success'}">${lote.estado}</span><br/>
            <small>Sector: ${lote.sector} | Fila: ${lote.fila} | Col: ${lote.columna}</small>
            ${lote.difunto ? `<br/><small>👤 ${lote.difunto.nombre} ${lote.difunto.apellido}</small>` : ''}
        </div>`;

    const marcador = L.marker([lote.latitud, lote.longitud], { icon: icono })
        .addTo(mapa)
        .bindPopup(popupContent);

    marcador.on('click', () => mostrarInfoLote(lote));
    marcadores.push(marcador);
}

// Limpiar todos los marcadores del mapa
function limpiarMarcadores() {
    marcadores.forEach(m => mapa.removeLayer(m));
    marcadores = [];
}

// Mostrar información detallada del lote en el panel lateral
function mostrarInfoLote(lote) {
    const contenido = document.getElementById('infoContenido');

    let html = `
        <div class="lote-detalle">
            <p class="mb-1"><strong>Lote:</strong> ${lote.identificador}</p>
            <p class="mb-1"><strong>Sector:</strong> ${lote.sector}</p>
            <p class="mb-1"><strong>Fila:</strong> ${lote.fila}</p>
            <p class="mb-1"><strong>Columna:</strong> ${lote.columna}</p>
            <p class="mb-1"><strong>Estado:</strong> 
                <span class="badge ${lote.estado === 'Ocupado' ? 'bg-danger' : 'bg-success'}">${lote.estado}</span>
            </p>
            ${lote.descripcion ? `<p class="mb-1"><strong>Descripción:</strong> ${lote.descripcion}</p>` : ''}
        </div>`;

    if (lote.difunto) {
        html += `
            <hr class="my-2"/>
            <div class="difunto-detalle">
                <p class="mb-1 fw-semibold">👤 Difunto</p>
                <p class="mb-1"><strong>Nombre:</strong> ${lote.difunto.nombre} ${lote.difunto.apellido}</p>
                ${lote.difunto.cedula ? `<p class="mb-1"><strong>Cédula:</strong> ${lote.difunto.cedula}</p>` : ''}
                <p class="mb-1"><strong>Nacimiento:</strong> ${lote.difunto.fechaNacimiento}</p>
                <p class="mb-1"><strong>Fallecimiento:</strong> ${lote.difunto.fechaFallecimiento}</p>
                ${lote.difunto.observaciones ? `<p class="mb-1"><strong>Observaciones:</strong> ${lote.difunto.observaciones}</p>` : ''}
            </div>`;
    } else {
        html += `<hr class="my-2"/><p class="text-muted">Este lote está disponible.</p>`;
    }

    contenido.innerHTML = html;
}

// Mostrar notificación temporal
function mostrarNotificacion(mensaje, tipo = 'success') {
    const notif = document.getElementById('notificacion');
    notif.textContent = mensaje;
    notif.className = `notificacion notificacion-${tipo}`;
    notif.style.display = 'block';
    setTimeout(() => { notif.style.display = 'none'; }, 3000);
}

// Event listeners
document.addEventListener('DOMContentLoaded', () => {
    inicializarMapa();

    document.getElementById('btnFiltrar').addEventListener('click', () => {
        const sector = document.getElementById('filtroSector').value;
        const estado = document.getElementById('filtroEstado').value;
        cargarLotes(sector, estado);
    });

    document.getElementById('btnLimpiar').addEventListener('click', () => {
        document.getElementById('filtroSector').value = '';
        document.getElementById('filtroEstado').value = '';
        cargarLotes();
    });
});
