var map;
var marker;
var infowindow;
var messagewindow;

function initMap() {
    var uluru = { lat: 42.6977, lng: 23.3219 };
    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 4,
        center: uluru
    });

    var marker = new google.maps.Marker({
        position: uluru,
        map: map
    });

    $("#Latitude").val(marker.getPosition().lat());
    $("#Longtitude").val(marker.getPosition().lng());

    infowindow = new google.maps.InfoWindow({
        content: document.getElementById('form')
    });

    messagewindow = new google.maps.InfoWindow({
        content: document.getElementById('message')
    });

    google.maps.event.addListener(map, 'click', function (event) {
        marker.setMap(null);
        marker = new google.maps.Marker({
            position: event.latLng,
            map: map
        });

        $("#Latitude").val(marker.getPosition().lat());
        $("#Longtitude").val(marker.getPosition().lng());

        google.maps.event.addListener(marker, 'click', function () {
            infowindow.open(map, marker);
        });
    });
}

