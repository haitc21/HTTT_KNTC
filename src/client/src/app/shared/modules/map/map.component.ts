import {
  AfterViewInit,
  Component,
  ElementRef,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
  ViewChild,
} from '@angular/core';
import * as L from 'leaflet';
import 'leaflet.locatecontrol';
import { Complain, typesHoSo } from '../../mock/Complain';

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

export class MapComponent implements AfterViewInit, OnChanges {
  @Input() idMap: string = 'map';
  @Input() data: Complain[] = [];
  @Input() spatialData: [];
  @Input() heightMap: string = '600px';
  @Input() zoomLv: number = 13;

  map: L.Map;

  vitri: any;
  khonggian: any;

  loaiHS = ['khiếu nại', 'Tố cáo'];
  linhVuc = ['Đất đai', 'Môi trường', 'Tài nguyên nước', 'Khoáng sản'];

  constructor() {}
  ngAfterViewInit() {
    this.initMap();
    this.buildLocateBtn();
    this.buildEventMapClick();
    this.renderMarkers(this.data);
    this.renderSpatialData(this.spatialData);
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes.data && changes.data.currentValue && !changes.data.isFirstChange()) {
      this.renderMarkers(changes.data.currentValue);
    }

    if (changes.spatialData && changes.spatialData.currentValue && !changes.spatialData.isFirstChange()) {
      this.renderSpatialData(changes.spatialData.currentValue);
    }
  }

  initMap() {
    this.map = L.map(this.idMap).setView([21.027764, 105.83416], this.zoomLv);
    this.vitri = L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      attribution:
        'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>',
      maxZoom: 18,
    }).addTo(this.map);
 
    /*
    this.vitri = L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      attribution:
        'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>',
      maxZoom: 18,
    })
    */
    this.khonggian = L.geoJSON();
    var myStyle = {
      "color": "#ff7800",
      "weight": 1,
      "opacity": 0.65
    };
    this.khonggian = L.geoJSON(this.spatialData, {
      style: myStyle
    }).addTo(this.map);
    //this.map = L.map(this.idMap).setView([21.027764, 105.83416], this.zoomLv, layers: [this.vitri, this.hinhhoc]);
    /*
    this.map = L.map(this.idMap, {
      center: [21.027764, 105.83416],
      zoom: this.zoomLv,
      layers: [this.vitri, this.khonggian]
    });
    */
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

  renderMarkers(hosos: Complain[]) {
    this.map.eachLayer(layer => {
      if (!(layer instanceof L.TileLayer)) {
        this.map.removeLayer(layer);
      }
    });

    //Add markers
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

  renderSpatialData(khonggian: []) {
    //Add polygons
    //this.spatialData 

    this.khonggian = L.geoJSON(this.spatialData).addTo(this.map);
  }
}
