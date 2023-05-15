import {
  AfterViewInit,
  ApplicationRef,
  Component,
  ComponentFactoryResolver,
  ComponentRef,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
  ViewContainerRef,
} from '@angular/core';
import { LoaiVuViec } from '@proxy';
import * as L from 'leaflet';
import { v4 as uuidv4 } from 'uuid';
import { SummaryDto} from '@proxy/summaries';
import { MapPopupComponent } from '../map-popup/map-popup.component';
import "leaflet.markercluster/dist/leaflet.markercluster";
import 'leaflet.markercluster.layersupport';
//import "leaflet-draw/dist/leaflet.draw.css";
//import "leaflet-draw/dist/leaflet.draw.js";
import "leaflet-draw";
import { format } from 'date-fns';
//import 'leaflet.locatecontrol';
//change projection - 0 cần projection nữa
//import "leaflet/dist/leaflet.css";
//import "proj4leaflet";
//import "proj4/dist/proj4.js";
//import "proj4leaflet/src/proj4leaflet.js";
//import { Proj4GeoJSONFeature } from 'proj4leaflet';
//import proj4 from 'proj4'
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
  @Input() zoomLv: number = 10;
  @Input() duLieuToaDo: string;  
  @Input() duLieuHinhhoc: string;  
  @Input() loaiVuViec: LoaiVuViec = LoaiVuViec.KhieuNai;

  idMap: string = uuidv4();
  map: L.Map;

  myStyle: any;

  //Cac layer tren ban do
  markers: any;
  info: any;
  khieunai: any;
  tocao: any;
  quyhoach: any;
  drawningBoard: any;

  // @ViewChild('popup', { read: ViewContainerRef }) popupContainer: ViewContainerRef;
  private popupComponentRef: ComponentRef<MapPopupComponent>;

  constructor(
    private componentFactoryResolver: ComponentFactoryResolver,
    private popupContainer: ViewContainerRef,
    private appRef: ApplicationRef
  ) {}

  ngAfterViewInit() {
    this.initMap();
    //this.buildLocateBtn();
    //this.buildEventMapClick();
    this.renderMarkers(this.data);
    this.renderSpatialData(this.spatialData);
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes.data && 
      changes.data.currentValue &&
      !changes.data.isFirstChange()) 
        this.renderMarkers(changes.data.currentValue);


    if (changes.spatialData &&
      changes.spatialData.currentValue &&
      !changes.spatialData.isFirstChange())
        this.renderSpatialData(changes.spatialData.currentValue);
  }

  initMap() {
    //Khoi tao ban do
    var center: L.LatLng = this.duLieuToaDo
      ? this.convertStringCoordiate(this.duLieuToaDo)
      : L.latLng(21.6825, 105.8442);
    
    this.map = L.map(this.idMap, {
    }).setView(center, this.zoomLv);

    // GeoJSON layer (UTM15)
    //proj4.defs("EPSG:9213","+proj=tmerc +lat_0=0 +lon_0=106.5 +k=0.9999 +x_0=500000 +y_0=0 +ellps=WGS84 +towgs84=-191.90441429,-39.30318279,-111.45032835,-0.00928836,0.01975479,-0.00427372,0.252906278 +units=m +no_defs +type=crs");
    
    const mbAttr =
      'Map data &copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>';
    const mbUrl =
      'https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4NXVycTA2emYycXBndHRqcmZ3N3gifQ.rJcFIG214AriISLbB6B5aw';

    const osm = L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
      maxZoom: 19,
      attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>',
    }).addTo(this.map);

    const streets = L.tileLayer(mbUrl, {
      id: 'mapbox/streets-v11',
      tileSize: 512,
      zoomOffset: -1,
      attribution: mbAttr,
    });
    
    const satellite = L.tileLayer(mbUrl, {
      id: 'mapbox/satellite-v9',
      tileSize: 512,
      zoomOffset: -1,
      attribution: mbAttr,
    });
    
    let baseMaps = {
      OpenStreetMap: osm,
      Streets: streets,
      Satellite: satellite
    };

    this.myStyle = {
      fillColor: '#ff0000',
      weight: 4,
      color: '#0000ff',
      opacity: 0.35
    };
    
    this.khieunai = L.layerGroup().addTo(this.map);
    this.tocao = L.layerGroup().addTo(this.map);    
    this.quyhoach = L.layerGroup().addTo(this.map);//empty layer

    var mcgLayerSupportGroup = L.markerClusterGroup.layerSupport({
      maxClusterRadius: 10,
      iconCreateFunction: function (cluster) {
				//var markers = cluster.getAllChildMarkers();

				return L.divIcon({ html: cluster.getChildCount(), className: 'mycluster', iconSize: L.point(40, 40) });
			},
			//Disable all of the defaults:
			spiderfyOnMaxZoom: false, showCoverageOnHover: false, zoomToBoundsOnClick: false,
      animate: true,
      animateAddingMarkers: false,
      spiderfyDistanceMultiplier: 1,
      //Options to pass to the L.Polygon constructor
		  polygonOptions: {}
    });
    mcgLayerSupportGroup.addLayer(this.khieunai);
    mcgLayerSupportGroup.addLayer(this.tocao);
    mcgLayerSupportGroup.addTo(this.map);

    mcgLayerSupportGroup.checkIn([this.khieunai, this.tocao]);
    //set boundary of Tỉnh Thái Nguyên
    //const overlay_ThaiNguyen: Proj4GeoJSONFeature = {
    const overlay_ThaiNguyen = {
      "type":"Feature",
      //"crs":{"type":"name","properties":{"name":"urn:ogc:def:crs:OGC:1.3:CRS84"}},
      "properties":{"COUNTRY":"Vietnam","NAME":"Thái Nguyên","VARNAME":"ThaiNguyen"},
      "geometry":{
        "type":"MultiPolygon",
        "coordinates":[[
          [[105.8918,21.3255],[105.8755,21.3278],[105.8717,21.3297],[105.8686,21.3268],[105.863,21.338],[105.8586,21.3382],[105.8575,21.3398],[105.8559,21.345],[105.8589,21.3457],[105.8524,21.3491],[105.8547,21.3523],[105.8517,21.3555],[105.8546,21.3676],[105.8592,21.3666],[105.8577,21.3711],[105.8589,21.3742],[105.8562,21.3765],[105.8494,21.3743],[105.8472,21.3774],[105.8397,21.3774],[105.8352,21.3733],[105.832,21.3733],[105.8303,21.3757],[105.8214,21.378],[105.8164,21.383],[105.8138,21.3828],[105.8127,21.3804],[105.8012,21.3852],[105.7996,21.3819],[105.7966,21.3827],[105.7967,21.3809],[105.788,21.3757],[105.7901,21.3733],[105.7873,21.3711],[105.7846,21.3597],[105.7752,21.3607],[105.7749,21.367],[105.773,21.3705],[105.77,21.3705],[105.7684,21.3747],[105.7598,21.3781],[105.7624,21.3857],[105.7656,21.3897],[105.7631,21.3996],[105.7556,21.4104],[105.7451,21.4159],[105.7403,21.415],[105.7342,21.4102],[105.7296,21.4089],[105.7209,21.4159],[105.7241,21.4216],[105.7246,21.4321],[105.723,21.4329],[105.7135,21.4302],[105.7077,21.4351],[105.7045,21.4407],[105.6975,21.439],[105.6874,21.4428],[105.6782,21.4559],[105.6749,21.4581],[105.6695,21.4578],[105.6667,21.4599],[105.6526,21.4573],[105.644,21.47],[105.6313,21.4791],[105.6329,21.4884],[105.6353,21.4928],[105.623,21.4988],[105.6185,21.4986],[105.6173,21.5023],[105.6117,21.5057],[105.6115,21.5122],[105.6069,21.5173],[105.6074,21.5228],[105.6043,21.5283],[105.5995,21.5323],[105.5839,21.535],[105.5831,21.54],[105.5637,21.5571],[105.5557,21.5709],[105.553,21.5723],[105.5456,21.5722],[105.5359,21.5814],[105.5267,21.5826],[105.5236,21.5895],[105.5141,21.6028],[105.5104,21.6047],[105.5056,21.6124],[105.5003,21.6143],[105.4966,21.6184],[105.4999,21.6326],[105.4976,21.6387],[105.4903,21.6437],[105.4883,21.6515],[105.4836,21.6597],[105.482,21.6718],[105.4793,21.6741],[105.4817,21.6787],[105.4871,21.6808],[105.4879,21.6887],[105.4775,21.6971],[105.4841,21.7021],[105.4903,21.7038],[105.4942,21.716],[105.5004,21.7175],[105.4994,21.723],[105.5042,21.7298],[105.5087,21.731],[105.5158,21.7364],[105.5163,21.74],[105.5113,21.7444],[105.509,21.7504],[105.5135,21.7546],[105.5186,21.7561],[105.5258,21.7678],[105.516,21.7742],[105.5003,21.7705],[105.4949,21.7742],[105.4934,21.7796],[105.4979,21.7931],[105.4931,21.8045],[105.4951,21.8174],[105.4938,21.8282],[105.4968,21.8319],[105.5108,21.8327],[105.5146,21.8272],[105.5168,21.8267],[105.52,21.8308],[105.528,21.8349],[105.5286,21.8524],[105.5225,21.8672],[105.5226,21.87],[105.5184,21.8759],[105.5209,21.8847],[105.5249,21.8879],[105.5323,21.8879],[105.5358,21.8903],[105.5415,21.8904],[105.5445,21.8922],[105.5469,21.8968],[105.545,21.904],[105.5529,21.9085],[105.5534,21.9184],[105.5507,21.9221],[105.5518,21.9436],[105.5491,21.9476],[105.5557,21.9575],[105.5645,21.9557],[105.5701,21.9578],[105.5755,21.9577],[105.5724,21.9666],[105.5794,21.9637],[105.5824,21.9663],[105.5778,21.9749],[105.5781,21.9771],[105.582,21.9806],[105.5794,21.985],[105.5832,21.9916],[105.5827,21.9944],[105.587,22.0035],[105.5904,22.0077],[105.5996,22.0102],[105.602,22.0141],[105.6082,22.0114],[105.6145,22.0136],[105.6165,22.0124],[105.6207,22.0184],[105.6239,22.0171],[105.6297,22.0191],[105.6362,22.0191],[105.635,22.0212],[105.6374,22.0214],[105.6405,22.027],[105.6456,22.0249],[105.6498,22.0251],[105.6537,22.0255],[105.658,22.0297],[105.6618,22.0293],[105.6633,22.0397],[105.6682,22.0436],[105.6689,22.0476],[105.6748,22.0472],[105.6923,22.0393],[105.6962,22.036],[105.6972,22.0311],[105.7072,22.023],[105.7154,22.0219],[105.7206,22.0245],[105.7274,22.0211],[105.7314,22.0153],[105.7394,22.019],[105.7454,22.0165],[105.7473,22.0137],[105.7534,22.0128],[105.7556,22.0108],[105.756,21.9922],[105.7635,21.9857],[105.7637,21.9809],[105.7608,21.9765],[105.762,21.9732],[105.7603,21.9722],[105.7607,21.9684],[105.7677,21.9607],[105.7691,21.957],[105.7753,21.953],[105.7744,21.949],[105.7763,21.9426],[105.774,21.9392],[105.7777,21.9349],[105.7771,21.9311],[105.771,21.9259],[105.7708,21.9218],[105.7672,21.9196],[105.7642,21.9147],[105.7716,21.9091],[105.7728,21.8962],[105.7813,21.8891],[105.7728,21.8873],[105.7703,21.8853],[105.772,21.8845],[105.7729,21.8756],[105.7702,21.8731],[105.7704,21.8703],[105.7683,21.8676],[105.7699,21.8612],[105.7689,21.8565],[105.7652,21.8522],[105.766,21.8503],[105.764,21.8448],[105.7605,21.844],[105.7612,21.8381],[105.7587,21.8356],[105.7579,21.8315],[105.7598,21.8309],[105.7624,21.8254],[105.7594,21.8188],[105.7674,21.813],[105.7788,21.811],[105.7864,21.8045],[105.7991,21.8051],[105.8009,21.807],[105.8005,21.8101],[105.803,21.818],[105.8018,21.8225],[105.8043,21.826],[105.805,21.8336],[105.8099,21.8391],[105.8129,21.84],[105.8151,21.8439],[105.825,21.8464],[105.8535,21.8439],[105.8557,21.8459],[105.8607,21.8459],[105.8646,21.8513],[105.8629,21.8606],[105.8724,21.8558],[105.8816,21.8587],[105.8889,21.8684],[105.8923,21.8764],[105.8985,21.8809],[105.9109,21.8957],[105.9186,21.896],[105.9273,21.894],[105.9366,21.8997],[105.9467,21.9028],[105.9558,21.9092],[105.9596,21.9136],[105.9613,21.9223],[105.9709,21.9246],[105.9691,21.9283],[105.9702,21.9349],[105.9744,21.9395],[105.9815,21.936],[105.9863,21.9364],[105.9952,21.9298],[106.0007,21.931],[106.0039,21.9351],[106.0127,21.9378],[106.0194,21.9475],[106.0191,21.9545],[106.0224,21.9571],[106.0243,21.9642],[106.0299,21.9678],[106.0334,21.9644],[106.0402,21.9626],[106.0437,21.9598],[106.0466,21.9604],[106.0467,21.9569],[106.0518,21.9508],[106.0534,21.9435],[106.0598,21.9414],[106.076,21.9426],[106.0901,21.9399],[106.0942,21.9416],[106.0996,21.9369],[106.1037,21.9366],[106.1069,21.9329],[106.111,21.9338],[106.1134,21.9291],[106.1178,21.9284],[106.1219,21.9242],[106.1276,21.9249],[106.1313,21.9215],[106.1318,21.9162],[106.1271,21.9105],[106.1297,21.9049],[106.1274,21.8994],[106.1159,21.8951],[106.1134,21.8907],[106.1143,21.8842],[106.1174,21.8784],[106.1153,21.8756],[106.1138,21.8672],[106.1115,21.8653],[106.1106,21.8608],[106.113,21.8541],[106.1114,21.8504],[106.1175,21.8435],[106.1123,21.84],[106.1067,21.8409],[106.1078,21.832],[106.1003,21.8285],[106.0997,21.8243],[106.095,21.8202],[106.0989,21.8128],[106.1077,21.8034],[106.1064,21.7903],[106.1112,21.7871],[106.1119,21.7816],[106.117,21.7825],[106.1186,21.7807],[106.1222,21.7834],[106.1371,21.785],[106.1393,21.7778],[106.1485,21.767],[106.1497,21.761],[106.1539,21.7615],[106.1596,21.756],[106.1684,21.7542],[106.1686,21.7498],[106.1736,21.7492],[106.1782,21.7437],[106.1817,21.7433],[106.1824,21.7416],[106.1944,21.7416],[106.1946,21.738],[106.1967,21.7354],[106.2076,21.7329],[106.2113,21.7284],[106.2184,21.7271],[106.225,21.7331],[106.2295,21.7306],[106.2309,21.7277],[106.2351,21.7272],[106.2363,21.7242],[106.2378,21.7159],[106.2337,21.7077],[106.2328,21.6989],[106.2244,21.6921],[106.2265,21.684],[106.2255,21.679],[106.2191,21.6735],[106.2187,21.6714],[106.2139,21.6697],[106.2058,21.6635],[106.2068,21.6601],[106.2043,21.6555],[106.194,21.6491],[106.191,21.6451],[106.1924,21.6373],[106.1985,21.6326],[106.1947,21.6268],[106.1973,21.6189],[106.1852,21.6043],[106.1739,21.6095],[106.1696,21.6165],[106.1595,21.6263],[106.1545,21.6239],[106.1421,21.6231],[106.1356,21.6179],[106.127,21.6161],[106.1169,21.617],[106.1072,21.6223],[106.1016,21.622],[106.0993,21.6252],[106.094,21.6247],[106.0922,21.6223],[106.0798,21.6189],[106.076,21.6126],[106.07,21.6093],[106.0673,21.6046],[106.0569,21.603],[106.0516,21.5923],[106.053,21.5896],[106.0486,21.5811],[106.0332,21.5714],[106.0336,21.568],[106.0321,21.5663],[106.0335,21.5642],[106.0304,21.562],[106.0336,21.5601],[106.041,21.561],[106.0463,21.5581],[106.048,21.5588],[106.0506,21.5521],[106.0524,21.5527],[106.0561,21.5491],[106.0539,21.5473],[106.0531,21.5487],[106.0505,21.5457],[106.0525,21.5427],[106.0521,21.5398],[106.0537,21.5387],[106.0474,21.5305],[106.0439,21.5208],[106.0446,21.5185],[106.0409,21.5163],[106.0419,21.5142],[106.045,21.5135],[106.0455,21.5086],[106.043,21.4988],[106.0378,21.4902],[106.0479,21.4817],[106.0455,21.4751],[106.0407,21.4727],[106.0437,21.4638],[106.0459,21.4648],[106.0483,21.4626],[106.052,21.4624],[106.0463,21.4421],[106.0385,21.4382],[106.0358,21.4394],[106.032,21.438],[106.0254,21.4313],[106.0311,21.4253],[106.033,21.4188],[106.0321,21.4136],[106.0344,21.4102],[106.0344,21.4083],[106.0312,21.4069],[106.03,21.4018],[106.0275,21.3994],[106.0293,21.3941],[106.0258,21.3947],[106.0246,21.3929],[106.0201,21.3955],[106.0176,21.4028],[106.0131,21.4028],[106.0086,21.406],[106.0026,21.4065],[106.0008,21.4047],[105.9956,21.4048],[105.9963,21.4081],[105.9907,21.4102],[105.9909,21.4129],[105.9867,21.4141],[105.9849,21.411],[105.9792,21.4136],[105.9757,21.4193],[105.9721,21.42],[105.9653,21.4357],[105.9612,21.4363],[105.9576,21.4322],[105.9586,21.4238],[105.9544,21.4132],[105.9589,21.4022],[105.955,21.4009],[105.9559,21.4049],[105.9534,21.4068],[105.945,21.4],[105.9422,21.4013],[105.94,21.3991],[105.9405,21.396],[105.9451,21.3912],[105.9434,21.377],[105.9385,21.3714],[105.9231,21.3749],[105.917,21.3726],[105.9115,21.3762],[105.9093,21.3746],[105.9043,21.3628],[105.8956,21.3511],[105.8986,21.3496],[105.9076,21.3523],[105.9086,21.3511],[105.9074,21.3486],[105.9044,21.3454],[105.896,21.343],[105.8865,21.346],[105.8808,21.3381],[105.8879,21.3282],[105.8928,21.3265],[105.8918,21.3255]]
        ]]
      }
    }

    let KNTC = {
      'Khiếu nại': this.khieunai,
      'Tố cáo': this.tocao,
    };

    //load base map and KN, TC first
    const layerControl = L.control.layers(baseMaps, KNTC).addTo(this.map);
    //load layer Thai Nguyen province's boundary
    //var overlay_TN = L.Proj.geoJson(overlay_ThaiNguyen).addTo(this.map);
    var overlay_TN = L.geoJson(overlay_ThaiNguyen).addTo(this.map);
    //add layer quy hoach (if any)
    //this.quyhoach.addTo(this.map);

    //Bổ sung các custom control cho Map: 
    //info control layer
    this.info = L.control();
    this.info.onAdd = function (map) {
      this._div = L.DomUtil.create('div', 'leaflet-control-info'); // create a div with a class "info"
      this.update();
      return this._div;
    };
    
    // method that we will use to update the control based on feature properties passed
    this.info.update = function () {
        //buildInfoContent
        var infoContent = '<h5>Thông tin Khiếu nại/Tố cáo</h5>';
        if (this.data){        
          infoContent += this.buildInfoContent(this.data);
        }else {
          infoContent += '<h6>Bạn cần di chuyển con trỏ vào khu vực KN/TC để xem thông tin!</h6>';
        }
        this._div.innerHTML = infoContent;
    };
    
    this.info.buildInfoContent = function(props: any){
      var type = ((props.loaiVuViec==1)?'Khiếu nại':'Tố cáo');
      var infoContent = '<h6>Loại vụ việc: <span color="blue"><strong>' + type +  '</strong></span></h6>';
      infoContent += '<table>\
        <tr>\
          <th scope="row">\
          <td>Tiêu đề:</td><td>' + props.tieuDe + '</td>\
        </tr>\
        <tr>\
          <th scope="row">\
          <td>Mã hồ sơ:</td><td>' + props.maHoSo + '</td>\
        </tr>\
        <tr>\
          <th scope="row">\
          <td>Người ' + type +  ':</td><td>' + props.nguoiNopDon + '</td>\
        </tr>\
        <tr>\
          <th scope="row">\
          <td>Ngày nhận đơn:</td><td>' + props.thoiGianTiepNhan + '</td>\
        </tr>\
        <tr>\
          <th scope="row">\
          <td>Ngày hẹn trả kết quả:</td><td>' + props.thoiGianHenTraKQ + '</td>\
        </tr>\
        <tr>\
          <th scope="row">\
          <td>Bộ phận đang XL:</td><td>' + props.boPhanDangXL + '</td>\
        </tr>\
      </table>';
      return infoContent;
    }

    this.info.addTo(this.map);

    //tạo Custom legend layer
    var legend = L.control({position: 'bottomright'});

    legend.onAdd = function (map) {

        var div = L.DomUtil.create('div', 'leaflet-control-info legend'),
            types = [1, 2],
            labels = ['Khiếu nại', 'Tố cáo'],
            bgColors = ['#2880ca' , '#ed5565']

        // loop through our density intervals and generate a label with a colored square for each interval
        for (var i = 0; i < types.length; i++) {
            div.innerHTML +=
                '<i style="background:' + bgColors[i] + '"></i> ' +
                labels[i] + '<br>';
        }

        return div;
    };

    legend.addTo(this.map);

    // Thêm layer cho Leaflet.Draw
    this.drawningBoard = new L.FeatureGroup();
    this.drawningBoard.addTo(this.map);
    //this.map.addLayer(this.drawningBoard);
  }

  letCoordinate() {
    this.map.on('click', e => {
      this.duLieuToaDo = `${e.latlng.lat}, ${e.latlng.lng}`;
      
      L.tooltip()
        .setLatLng(e.latlng)
        .setContent(
          `<h5>Đã xác nhận tọa độ tại vị trí: </h5> </br> <p>Kinh độ: ${e.latlng.lat}, Vĩ độ: ${e.latlng.lng} </p>`
        )
        .addTo(this.map);
      
    });

  }

  letDraw() {
    var editableLayer = this.drawningBoard;
    var options = {
      position: 'topleft',
      draw: {
        polyline: {
          shapeOptions: {
            color: '#f357a1',
            weight: 10
          }
        },
        polygon: {
          allowIntersection: false, // Restricts shapes to simple polygons
          drawError: {
            color: '#e1e100', // Color the shape will turn when intersects
            message: '<strong>Oh snap!<strong> you can\'t draw that!' // Message that will show when intersect
          },
          shapeOptions: {
            color: '#bada55'
          }
        },
        circle: {
          shapeOptions: {
            clickable: false
          }
        },
        rectangle: {
          showArea: false,
          shapeOptions: {
            clickable: false
          }
        },
  
      },
      edit: {
        featureGroup: editableLayer, //REQUIRED!!
        remove: false
      }
    };
    var drawControl = new L.Control.Draw(options);
    this.map.addControl(drawControl);
    
    // Bắt sự kiện khi người dùng kết thúc vẽ một hình dạng
    this.map.on("draw:created", (e) => {
      var type = e.layerType;
      var layer = e.layer;

      if (type != 'marker' && type != 'circlemarker') {
        editableLayer.addLayer(layer);

        // Chuyển đổi layer sang GeoJSON và trả về cho người dùng
        var geojson = layer.toGeoJSON();
        this.duLieuHinhhoc = JSON.stringify(geojson);
        var point: any;
        if (type == "circle")
          point = layer.getLatLng();
        else{
          var latlngs  = layer.getLatLngs();

          if(!L.LineUtil.isFlat(latlngs)){
            latlngs = latlngs[0];
          }
          point = latlngs[0];
        }

        L.tooltip()
          .setLatLng(point)
          .setContent(
            `<h5>Đã xác nhận dữ liệu hình học mới: </h5> </br> <p>${this.duLieuHinhhoc}</p>`
          )
          .addTo(this.map);
        //console.log(geojson);
      }
      else{//Chỉ cho duy nhất 1 marker
        var e = layer.getLatLng();
        this.duLieuToaDo = `${e.latlng.lat}, ${e.latlng.lng}`;
      
        L.tooltip()
          .setLatLng(e.latlng)
          .setContent(
            `<h5>Đã xác nhận tọa độ tại vị trí: </h5> </br> <p>Kinh độ: ${e.latlng.lat}, Vĩ độ: ${e.latlng.lng} </p>`
          )
          .addTo(this.map);
      }
    });
    //map.on("draw:created", function (e) { //L.Draw.Event.CREATED
    //map.on('draw:drawstop',  (e) => { console.log("end");
  }

  renderMarkers(data: SummaryDto[]) {
    //clear all markers
    this.khieunai.clearLayers();
    this.tocao.clearLayers();

    let customOptions = {
      minWidth: 500,
      maxWidth: 800,      
      maxHeight: 400,
      className: 'popupCustom',
      closeOnEscapeKey: true,
    };
    //Add markers
    data
      .filter(x => x.duLieuToaDo != null || x.duLieuHinhHoc != null)
      .forEach(dataMap => {
        let toado: L.LatLng = new L.LatLng(0 , 0);
        let geometry: any;
        let geojson: any;
        if (dataMap.duLieuToaDo != null)//Có tọa độ thì lấy tọa độ
          toado = this.convertStringCoordiate(dataMap.duLieuToaDo);
        else{//Không có tọa độ thì lấy điểm đầu tiên trong hình học
          //geometry = JSON.parse(dataMap.duLieuHinhHoc);
          geojson = JSON.parse(dataMap.duLieuHinhHoc);
          var coordinates = geojson.geometry.coordinates;
          //var coordinates = geometry.coordinates;
          toado = coordinates[0];
        }

        //always add marker
        var info = this.info;
        //var pros = this.buildProperties(dataMap);
        //var infoContent = this.buildInfoContent(pros);

        const marker = L.marker(toado, {
          icon: dataMap.loaiVuViec === LoaiVuViec.KhieuNai ? blueIcon : redIcon
        });
        
        //marker.data = dataMap;
        marker.on('mouseover',function(e) {
          info.data = dataMap;
          info.update();
        });
        
        marker.bindPopup(this.getPopup(dataMap), customOptions);

        if (dataMap.loaiVuViec == LoaiVuViec.KhieuNai) marker.addTo(this.khieunai);
        else if (dataMap.loaiVuViec == LoaiVuViec.ToCao) marker.addTo(this.tocao);
        
        //Add duLieuHinhHoc if any
        if (dataMap.duLieuHinhHoc != null){
          //this.quyhoach.clearLayers();
          //if (geometry==undefined)
          //  geometry = JSON.parse(dataMap.duLieuHinhHoc);
          if (geojson==undefined)
            geojson = JSON.parse(dataMap.duLieuHinhHoc);
          //let geojson: Proj4GeoJSONFeature = 
          /*
          let geojson = 
          { 
            "id": dataMap.id,
            "type": "Feature", 
            //"properties": pros, 
            "geometry": geometry,
            //"crs": {"type":"name","properties":{"name":"urn:ogc:def:crs:EPSG::9213"}},
          }
          */
          /*
          if (dataMap.loaiVuViec == LoaiVuViec.KhieuNai) L.geoJson(geojson, {style: this.myStyle}).addTo(this.khieunai);
          else if (dataMap.loaiVuViec == LoaiVuViec.ToCao) L.geoJson(geojson, {style: this.myStyle}).addTo(this.tocao);  
          */
         
          //let newlayer = L.Proj.geoJson(geojson,
          let newlayer = L.geoJson(geojson,
          {
            style: (feature) => ({
              weight: 3,
              opacity: 0.5,
              color: '#008f68',
              fillOpacity: 0.8,
              fillColor: (dataMap.loaiVuViec==1)? '#2880ca': '#ed5565',
            }),
            onEachFeature: (feature, layer) => (
            layer.on({
              mouseover: (e) => (this.highlightFeature(layer, dataMap)),
              mouseout: (e) => (this.resetFeature(layer, dataMap.loaiVuViec)),
              onclick: (e) => (this.zoomToFeature(layer))
            })
          )
          });
          
          if (dataMap.loaiVuViec == LoaiVuViec.KhieuNai) 
            newlayer.addTo(this.khieunai);
          else if (dataMap.loaiVuViec == LoaiVuViec.ToCao) 
            newlayer.addTo(this.tocao);  
          //L.Proj.geoJson(geojson, {style: this.myStyle}).addTo(this.quyhoach);
        }
      });
  }

  renderSpatialData(khonggian: any[]) {
    //clear all layers
    this.quyhoach.clearLayers();

    if (khonggian!=undefined)
      khonggian.forEach(feat => {
        //let geojson: Proj4GeoJSONFeature = 
        let geojson = 
        {     
        "id": feat.id,
        "type": "Feature",
        "properties": {
            "popupContent": "<p>Tên tổ chức: " + feat.tenToChuc + "</p><p>Quyển: " + feat.quyen + "</p><p>Số tờ bản đồ: " + feat.soToBD + "</p>",
        },
        "geometry": JSON.parse(feat.geoJson),
        //"crs": {"type":"name","properties":{"name":"urn:ogc:def:crs:EPSG::9213"}},
        }
        //L.Proj.geoJson(geojson, {style: this.myStyle}).addTo(this.quyhoach);
        L.geoJson(geojson, {style: this.myStyle}).addTo(this.quyhoach);
      });
  }

  buildProperties(dataMap: SummaryDto){
    return {
      "tieuDe": "<p>" + dataMap.tieuDe + "</p>",
      "maHoSo": dataMap.maHoSo,
      "type": (dataMap.loaiVuViec == 1)?"Khiếu nại":"Tố cáo",
      "loaiVuViec": dataMap.loaiVuViec,
      "nguoiNopDon": dataMap.nguoiNopDon,
      "thoiGianTiepNhan": format(new Date(dataMap.thoiGianTiepNhan),'dd/MM/yyyy HH:mm'),
      "thoiGianHenTraKQ": format(new Date(dataMap.thoiGianHenTraKQ),'dd/MM/yyyy HH:mm'),
      "boPhanDangXL": dataMap.boPhanDangXL
    }
  }

  convertStringCoordiate(cor: string): L.LatLng {
    let point = cor.split(',');
    return new L.LatLng(+point[0], +point[1]);
  }

  getPopup(dataMap: SummaryDto): HTMLElement {
    const factory = this.componentFactoryResolver.resolveComponentFactory(MapPopupComponent);
    this.popupComponentRef = factory.create(this.popupContainer.injector);
    this.popupComponentRef.instance.dataMap = dataMap;
    this.appRef.attachView(this.popupComponentRef.hostView); // Đính kèm view của component
    return this.popupComponentRef.location.nativeElement;
  }

  private highlightFeature(layer, data) {
      //var layer = e.target;

      layer.setStyle({
          weight: 10,
          color: '#666',
          dashArray: '',
          fillOpacity: 0.7
      });

      layer.bringToFront();

      this.info.data = data;
      this.info.update();
  }

  private resetFeature(layer, type) {
    //const layer = e.target;
  
    layer.setStyle({
      weight: 3,
      opacity: 0.5,
      color: '#008f68',
      fillOpacity: 0.8,
      fillColor: (type==1)? '#2880ca': '#ed5565'
    });

    //info.update();
    //this.info.update();
  }

  private zoomToFeature(e) {
      this.map.fitBounds(e.target.getBounds());
  }
}
