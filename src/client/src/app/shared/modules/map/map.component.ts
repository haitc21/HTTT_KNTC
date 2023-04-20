import {
  AfterViewInit,
  ApplicationRef,
  Component,
  ComponentFactoryResolver,
  ComponentRef,
  ElementRef,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
  ViewChild,
  ViewContainerRef,
} from '@angular/core';
import { LoaiVuViec } from '@proxy';
import * as L from 'leaflet';
import 'leaflet.locatecontrol';
import { v4 as uuidv4 } from 'uuid';
import { SummaryDto, SummaryMapDto } from '@proxy/summaries';
import { MapPopupComponent } from '../map-popup/map-popup.component';

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
  @Input() data: SummaryMapDto[] = [];
  @Input() spatialData: any[];
  @Input() heightMap: string = '600px';
  @Input() zoomLv: number = 10;
  @Input() duLieuToaDo: string;
  @Input() loaiVuViec: LoaiVuViec = LoaiVuViec.KhieuNai;

  idMap: string = uuidv4();
  map: L.Map;

  // khieunai: any;
  // tocao: any;
  quyhoach: any;

  // @ViewChild('popup', { read: ViewContainerRef }) popupContainer: ViewContainerRef;
  private popupComponentRef: ComponentRef<MapPopupComponent>;

  constructor(
    private componentFactoryResolver: ComponentFactoryResolver,
    private popupContainer: ViewContainerRef,
    private appRef: ApplicationRef
  ) {}

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
    // this.khieunai = L.layerGroup();
    // this.tocao = L.layerGroup();

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
      : [21.5928, 105.8442];
    this.map = L.map(this.idMap, {
      center: center,
      zoom: this.zoomLv,
      // layers: [osm, this.khieunai],
      layers: [osm],
    });

    let baseMaps = {
      OpenStreetMap: osm,
      Streets: streets,
    };

    let geojsonFeature = {
      type: 'Feature',
      properties: {
        name: 'Dữ liệu quy hoạch',
        amenity: 'Thái Nguyên',
        popupContent: 'Xem dữ liệu quy hoạch!',
      },
      geometry: this.spatialData, // undefined vì chưa được load đã chạy câu này rồi
    };

    let myStyle = {
      fillColor: '#ff7800',
      weight: 2,
      opacity: 1,
      color: 'white',
      dashArray: '3',
      fillOpacity: 0.7,
    };

    this.quyhoach = L.geoJSON(geojsonFeature, { style: myStyle });

    let overlayMaps = {
      // 'Khiếu nại': this.khieunai,
      // 'Tố cáo': this.tocao,
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
    let myStyle = {
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
      
      L.tooltip()
        .setLatLng(e.latlng)
        .setContent(
          `<h5>Vị trí: </h5> </br> <p>Kinh độ: ${e.latlng.lat}, Vĩ độ: ${e.latlng.lng} </p>`
        )
        .openOn(this.map);
      
    });
  }

  renderMarkers(data: SummaryMapDto[]) {
    if (!data || data.length == 0) return;
    this.map.eachLayer(layer => {
      if (!(layer instanceof L.TileLayer)) {
        this.map.removeLayer(layer);
      }
    });
    let customOptions = {
      minWidth: 500,
      maxWidth: 800,      
      maxHeight: 400,
      className: 'popupCustom',
      closeOnEscapeKey: true,
    };
    //Add markers
    data
      .filter(x => x.duLieuToaDo != null)
      .forEach(dataMap => {
        const marker = L.marker(this.convertStringCoordiate(dataMap.duLieuToaDo), {
          icon: dataMap.loaiVuViec === LoaiVuViec.KhieuNai ? blueIcon : redIcon,
        });

        marker.bindPopup(this.getPopup(dataMap), customOptions);

        marker.addTo(this.map);
        // if (dataMap.loaiVuViec == LoaiVuViec.KhieuNai) marker.addTo(this.khieunai);
        // else if (dataMap.loaiVuViec == LoaiVuViec.ToCao) marker.addTo(this.tocao);
      });
  }

  renderSpatialData(khonggian: any[]) {
    //Add polygons
    //this.spatialData
    /*
    let myStyle = {
      "color": "#ff7800",
      "weight": 5,
      "opacity": 0.65
    };
    */
  }
  convertStringCoordiate(cor: string): [number, number] {
    let point = cor.split(',');
    return [+point[0], +point[1]];
  }
  getPopup(dataMap: SummaryMapDto): HTMLElement {
    const factory = this.componentFactoryResolver.resolveComponentFactory(MapPopupComponent);
    this.popupComponentRef = factory.create(this.popupContainer.injector);
    this.popupComponentRef.instance.dataMap = dataMap;
    this.appRef.attachView(this.popupComponentRef.hostView); // Đính kèm view của component
    return this.popupComponentRef.location.nativeElement;
  }
}
