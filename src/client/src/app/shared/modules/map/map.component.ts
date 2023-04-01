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
import { LoaiVuViec } from '@proxy';
import * as L from 'leaflet';
import 'leaflet.locatecontrol';
import { v4 as uuidv4 } from 'uuid';
import { SummaryDto } from '@proxy/summaries';

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
  @Input() data: SummaryDto[] = [];
  @Input() spatialData: any[];
  @Input() heightMap: string = '600px';
  @Input() zoomLv: number = 13;
  @Input() duLieuToaDo: string;

  idMap: string = uuidv4();
  map: L.Map;

  khieunai: any;
  tocao: any;
  quyhoach: any;

  constructor() {}

  ngAfterViewInit() {
    this.initMap();
    this.buildLocateBtn();
    this.buildEventMapClick();
    this.renderMarkers(this.data);
    this.renderSpatialData(this.spatialData);
    if (this.duLieuToaDo) {
      let marker = L.marker(this.convertStringCoordiate(this.duLieuToaDo), { icon: blueIcon });
      marker.addTo(this.map);
    }
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes.data && changes.data.currentValue && !changes.data.isFirstChange()) {
      this.renderMarkers(changes.data.currentValue);
    }

    if (
      changes.spatialData &&
      changes.spatialData.currentValue &&
      !changes.spatialData.isFirstChange()
    ) {
      this.renderSpatialData(changes.spatialData.currentValue);
    }
  }

  initMap() {
    /*
    this.map = L.map(this.idMap).setView([21.027764, 105.83416], this.zoomLv);
    this.vitri = L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      attribution:
        'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>',
      maxZoom: 18,
    }).addTo(this.map);
    */

    /*
    this.vitri = L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      attribution:
        'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>',
      maxZoom: 18,
    })
    */
    this.khieunai = L.layerGroup();
    this.tocao = L.layerGroup();

    const mbAttr =
      'Map data &copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>';
    const mbUrl =
      'https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4NXVycTA2emYycXBndHRqcmZ3N3gifQ.rJcFIG214AriISLbB6B5aw';

    const streets = L.tileLayer(mbUrl, {
      id: 'mapbox/streets-v11',
      tileSize: 512,
      zoomOffset: -1,
      attribution: mbAttr,
    });

    const osm = L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
      maxZoom: 19,
      attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>',
    });

    let center = this.duLieuToaDo
      ? this.convertStringCoordiate(this.duLieuToaDo)
      : [21.59053436945016, 105.83127034149382];
    this.map = L.map(this.idMap, {
      center: center,
      zoom: 10,
      layers: [osm, this.khieunai],
    });

    var baseMaps = {
      OpenStreetMap: osm,
      Streets: streets,
    };

    var geojsonFeature = {
      type: 'Feature',
      properties: {
        name: 'Dữ liệu quy hoạch',
        amenity: 'Thái Nguyên',
        popupContent: 'Xem dữ liệu quy hoạch!',
      },
      geometry: this.spatialData,
    };

    var myStyle = {
      fillColor: '#ff7800',
      weight: 2,
      opacity: 1,
      color: 'white',
      dashArray: '3',
      fillOpacity: 0.7,
    };

    this.quyhoach = L.geoJSON(geojsonFeature, { style: myStyle });

    var overlayMaps = {
      'Khiếu nại': this.khieunai,
      'Tố cáo': this.tocao,
      'Quy hoạch': this.quyhoach,
    };

    const layerControl = L.control.layers(baseMaps, overlayMaps).addTo(this.map);

    const satellite = L.tileLayer(mbUrl, {
      id: 'mapbox/satellite-v9',
      tileSize: 512,
      zoomOffset: -1,
      attribution: mbAttr,
    });
    layerControl.addBaseLayer(satellite, 'Satellite');

    //layerControl.addOverlay(this.quyhoach, 'Quy hoạch');

    /*
    //
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
      this.duLieuToaDo = `${e.latlng.lat}, ${e.latlng.lng}`;
      L.popup()
        .setLatLng(e.latlng)
        .setContent(
          `<h5>Vị trí: </h5> </br> <p>Kinh độ: ${e.latlng.lat}, Vĩ độ: ${e.latlng.lng} </p>`
        )
        .openOn(this.map);
    });
  }

  renderMarkers(data: SummaryDto[]) {
    this.map.eachLayer(layer => {
      if (!(layer instanceof L.TileLayer)) {
        this.map.removeLayer(layer);
      }
    });

    //Add markers
    data
      .filter(x => x.duLieuToaDo != null)
      .forEach(hoSo => {
        const marker = L.marker(this.convertStringCoordiate(hoSo.duLieuToaDo), {
          icon: hoSo.loaiVuViec === LoaiVuViec.KhieuNai ? blueIcon : redIcon,
        });

        var customOptions = {
          maxWidth: '400',
          width: '200',
          className: 'popupCustom',
        };

        marker.bindPopup(
          `
      <form name="myform" role="form" id="form" class="form-horizontal">
      <div class="form-group">
        <label class="control-label col-sm-2"><strong>Nội dung</strong></label>      
        <div class="col-sm-10"><b>${hoSo.tieuDe}</b></div>
      </div>
      <div class="form-group">
        <label class="control-label col-sm-2"><strong>Mã hồ sơ:</strong></label>
        <div class="col-sm-10">${hoSo.maHoSo}</div>
      </div>
      <div class="form-group">
        <label class="control-label col-sm-2"><strong>Người gửi đơn:</strong></label>
        <div class="col-sm-10">${hoSo.nguoiNopDon}</div>
      </div>
      <div class="form-group">
        <label class="control-label col-sm-2"><strong>Điện thoại:</strong></label>
        <div class="col-sm-10">${hoSo.dienThoai}</div>
      </div>
      <div class="form-group">
        <label class="control-label col-sm-2"><strong>Thời gian tiếp nhận:</strong></label>
        <div class="col-sm-10">${hoSo.thoiGianTiepNhan}</div>
      </div>
      <div class="form-group">
        <label class="control-label col-sm-2"><strong>Thời gian hẹn trả KQ:</strong></label>
        <div class="col-sm-10">${hoSo.thoiGianHenTraKQ}</div>
      </div>
      <div class="form-group">
        <label class="control-label col-sm-2"><strong>Bộ phận đang XL:</strong></label>
        <div class="col-sm-10">${hoSo.boPhanDangXL}</div>
      </div>
      `,
          customOptions
        );

        //marker.addTo(this.map);
        if (hoSo.loaiVuViec == LoaiVuViec.KhieuNai) marker.addTo(this.khieunai);
        else if (hoSo.loaiVuViec == LoaiVuViec.ToCao) marker.addTo(this.tocao);
      });
  }

  renderSpatialData(khonggian: any[]) {
    //Add polygons
    //this.spatialData
    /*
    var myStyle = {
      "color": "#ff7800",
      "weight": 5,
      "opacity": 0.65
    };
    */
  }
  convertStringCoordiate(cor: string): [number, number] {
    var point = cor.split(',');
    return [+point[0], +point[1]];
  }
}
