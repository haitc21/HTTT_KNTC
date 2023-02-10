import { AfterViewInit, Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import * as L from 'leaflet';
import 'leaflet.locatecontrol';
import { HoSo, typesHoSo } from '../../mock/HoSo';

const blueIcon = new L.Icon({
  iconUrl: 'assets/images/map/marker-icon.png',
  iconSize: [25, 41],
  iconAnchor: [12, 41],
  popupAnchor: [1, -34],
  shadowSize: [41, 41],
});
const redIcon = new L.Icon({
  iconUrl: 'assets/images/map/marker-icon-red.png',
  iconSize: [25, 41],
  iconAnchor: [12, 41],
  popupAnchor: [1, -34],
  shadowSize: [41, 41],
});

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss'],
})
export class MapComponent implements OnInit, OnChanges {
  @Input() data: HoSo[] = [];

  private map;

  loaiHS = ['khiếu nại', 'Tố cáo'];
  linhVuc = ['Đất đai', 'Môi trường', 'Tài nguyên nước', 'Khoáng sản'];

  constructor() {}
  ngOnInit(): void {
    this.initMap();
  }
  ngOnChanges(changes: SimpleChanges): void {
    if (changes.data && changes.data.currentValue && !changes.data.isFirstChange()) {
      console.log(this.data);

      this.renderMarkers(changes.data.currentValue);
    }
  }

  initMap() {
    this.map = L.map('map').setView([21.027764, 105.83416], 13);
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      attribution:
        'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>',
      maxZoom: 18,
    }).addTo(this.map);

    // nút định vị
    this.buildLocateBtn();
    this.buildEventMapClick();
  }

  buildLocateBtn() {
    return L.control
      .locate({
        position: 'topleft',
        drawCircle: true,
        follow: true,
        setView: true,
        keepCurrentZoomLevel: true,
        markerStyle: {
          weight: 1,
          opacity: 0.8,
          fillOpacity: 0.8,
        },
        circleStyle: {
          weight: 1,
          clickable: false,
        },
        icon: 'fa fa-map-marker',
        metric: true,
        strings: {
          title: 'Vị trí hiện tại',
          popup: 'Bạn cách đây {distance} {unit}',
          outsideMapBoundsMsg: 'Vị trí hiện tại năm ngoài bản đồ',
        },
        locateOptions: {
          maxZoom: 22,
          watch: true,
          enableHighAccuracy: true,
          maximumAge: 1000,
          timeout: 1000,
        },
      })
      .addTo(this.map);
  }
  buildEventMapClick() {
    this.map.on('click', e => {
      L.popup()
        .setLatLng(e.latlng)
        .setContent(
          `<b>Vị trí: </b> </br> <p>Kinh độ: ${e.latlng.lat}, Vĩ độ: ${e.latlng.lng} </p>`
        )
        .openOn(this.map);
    });
  }

  renderMarkers(hosos: HoSo[]) {
    this.map.eachLayer(layer => {
      if (!(layer instanceof L.TileLayer)) {
        this.map.removeLayer(layer);
      }
    });

    hosos.forEach(hoSo => {
      const marker = L.marker([hoSo.latLng[0], hoSo.latLng[1]], {
        icon: hoSo.typeHoSo === 0 ? blueIcon : redIcon,
      });

      marker.bindPopup(`
        <div>
          <h5>${hoSo.title}</h5>
          <p>Mã đơn: ${hoSo.code}</p>
          <p>Người gửi đơn: ${hoSo.sender}</p>
          <p>Khu vực: ${hoSo.area}</p>
          <p>Loại đơn: ${this.loaiHS[hoSo.typeHoSo]}</p>
          <p>Lĩnh vực: ${this.linhVuc[hoSo.fieldType]}</p>
        </div>
      `);
      marker.addTo(this.map);
    });
  }
}
