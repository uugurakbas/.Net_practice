function initializeMap() {
    var map = L.map('map').setView([41.0082, 28.9784], 12);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
    }).addTo(map);

    var mapElement = document.getElementById('map');
    var parkData = JSON.parse(mapElement.dataset.parks);

    function getCircleRadius(zoomLevel) {
        // Zoom seviyesine göre daire boyutunu ayarla
        return Math.max(5, 20 - zoomLevel);
    }

    function updateCircleSizes() {
        var currentZoom = map.getZoom();
        var radius = getCircleRadius(currentZoom);
        
        map.eachLayer(function(layer) {
            if (layer instanceof L.CircleMarker) {
                layer.setRadius(radius);
            }
        });
    }

    // Zoom değiştiğinde daire boyutlarını güncelle
    map.on('zoomend', updateCircleSizes);

    parkData.forEach(function(park) {
        var circle = L.circleMarker([park.latitude, park.longitude], {
            radius: getCircleRadius(map.getZoom()),
            color: '#3388ff',
            fillColor: '#3388ff',
            fillOpacity: 0.5,
            weight: 1
        }).addTo(map);

        circle.bindPopup(`
            <b>${park.parkName}</b><br>
            Kapasite: ${park.capacity}<br>
            Boş Alan: ${park.emptyCapacity}
        `);
    });
}

document.addEventListener('DOMContentLoaded', initializeMap); 