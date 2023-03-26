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
  @Input() spatialData: any[];
  @Input() heightMap: string = '600px';
  @Input() zoomLv: number = 13;

  map: L.Map;

  khieunai: any;
  tocao: any;
  quyhoach: any;

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

    const mbAttr = 'Map data &copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>';
    const mbUrl = 'https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4NXVycTA2emYycXBndHRqcmZ3N3gifQ.rJcFIG214AriISLbB6B5aw';
   
    const streets = L.tileLayer(mbUrl, {id: 'mapbox/streets-v11', tileSize: 512, zoomOffset: -1, attribution: mbAttr});
  
    const osm = L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
      maxZoom: 19,
      attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    });
  
    this.map = L.map(this.idMap, {
      center: [21.59053436945016, 105.83127034149382],
      zoom: 10,
      layers: [osm, this.khieunai]
    });
  
    var baseMaps  = {
      'OpenStreetMap': osm,
      'Streets': streets
    };
  
    var geojsonFeature = {
      "type": "Feature",
      "properties": {
          "name": "Dữ liệu quy hoạch",
          "amenity": "Thái Nguyên",
          "popupContent": "Xem dữ liệu quy hoạch!"
      },
      "geometry": this.spatialData
    };

    var myStyle = {
      fillColor: "#ff7800",
      weight: 2,
      opacity: 1,
      color: 'white',
      dashArray: '3',
      fillOpacity: 0.7
    };

    this.quyhoach = L.geoJSON(geojsonFeature,
      {style: myStyle
      });    

    var overlayMaps  = {
      'Khiếu nại': this.khieunai,
      'Tố cáo': this.tocao,
      'Quy hoạch': this.quyhoach
    };
  
    const layerControl = L.control.layers(baseMaps , overlayMaps ).addTo(this.map);    
    
    const satellite = L.tileLayer(mbUrl, {id: 'mapbox/satellite-v9', tileSize: 512, zoomOffset: -1, attribution: mbAttr});
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
      L.popup()
        .setLatLng(e.latlng)
        .setContent(
          `<b>Vị trí: </b> </br> <p>Kinh độ: ${e.latlng.lat}, Vĩ độ: ${e.latlng.lng} </p>`
        )
        .openOn(this.map);
    });
  }

  renderMarkers(hosos: any[]) {
    this.map.eachLayer(layer => {
      if (!(layer instanceof L.TileLayer)) {
        this.map.removeLayer(layer);
      }
    });
    debugger;
    //Add markers
    hosos.filter(x => x.duLieuToaDo!=null).forEach(hoSo => {
      var point = hoSo.duLieuToaDo.split(",");
      const marker = L.marker([point[0], point[1]], {
        icon: hoSo.type === 1 ? blueIcon : redIcon,
      });

      marker.bindPopup(`
        <h5>${hoSo.tieuDe}</h5>
        <div class="form-group row">
        <label class="col-sm-2 col-form-label">Mã hồ sơ:</label>
          <div class="col-sm-10">
            ${hoSo.maHoSo}
          </div>
        </div>
        <div>
          
          <p>Mã hồ sơ: </p>
          <p>Người gửi đơn: ${hoSo.nguoiDeNghi}</p>
          <p>CCCD/CCID: ${hoSo.cccdCmnd}</p>
          <p>Loại đơn: ${this.loaiHS[hoSo.typeHoSo]}</p>
          <p>Lĩnh vực: ${this.linhVuc[hoSo.fieldType]}</p>
        </div>
      `);
      //marker.addTo(this.map);
      if (hoSo.type==LoaiVuViec.KhieuNai)
        marker.addTo(this.khieunai);
      else if (hoSo.type==LoaiVuViec.ToCao)
        marker.addTo(this.tocao);
    });    
  }

  renderSpatialData(khonggian: any[]) {
    //Add polygons
    //this.spatialData 
    debugger;
    /*
    var myStyle = {
      "color": "#ff7800",
      "weight": 5,
      "opacity": 0.65
    };
    */
    
  }
}
