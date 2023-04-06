import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { SpatialDataDto, SpatialDataService, GetSpatialDataListDto } from '@proxy/spatial-datas';
import { Subject, takeUntil } from 'rxjs';
import { UnitLookupDto } from '@proxy/units/models';
import { GetSummaryListDto, SummaryDto } from '../../proxy/summaries/models';
import { SummaryService } from '@proxy/summaries';
import { MessageConstants } from 'src/app/shared/constants/messages.const';
import { ComplainDetailComponent } from '../complain/detail/complain-detail.component';
import { DenounceDetailComponent } from '../denounce/detail/denounce-detail.component';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  animations: [
    trigger('hideColumn', [
      state(
        'visible',
        style({
          width: '16.6666666667%',
          opacity: 1,
        })
      ),
      state(
        'hidden',
        style({
          width: 0,
          opacity: 0,
        })
      ),
      transition('visible => hidden', animate('0.3s')),
      transition('hidden => visible', animate('0.3s')),
    ]),
    trigger('expandColumn', [
      state(
        'normal',
        style({
          width: '83.3333333333%',
        })
      ),
      state(
        'expanded',
        style({
          width: '100%',
        })
      ),
      transition('normal => expanded', animate('0.3s')),
      transition('expanded => normal', animate('0.3s')),
    ]),
  ],
})
export class HomeComponent implements OnInit, OnDestroy {
  //System variables
  private ngUnsubscribe = new Subject<void>();
  home: MenuItem;

  blockedPanel = false;
  dataMap: SummaryDto[] = [];

  filter: GetSummaryListDto;

  landComplain = true;
  enviromentComplain = true;
  waterComplain = true;
  mineralComplain = true;

  landDenounce = true;
  enviromentDenounce = true;
  waterDenounce = true;
  mineralDenounce = true;

  // ẩn hiện menu trái
  visibleFilterLeff = true;
  hideColumnState = 'visible';
  expandColumnState = 'normal';

  get hasLoggedIn(): boolean {
    return this.oAuthService.hasValidAccessToken();
  }

  constructor(
    private oAuthService: OAuthService,
    private spatialDataService: SpatialDataService,
    private summaryService: SummaryService
  ) {}

  ngOnInit(): void {
    this.home = { label: ' Trang chủ', icon: 'pi pi-home', routerLink: '/' };
    this.loadData(true);
  }

  loadData(isFirst: boolean = false) {
    this.toggleBlockUI(true);
    this.filter = {
      landComplain: this.landComplain,
      enviromentComplain: this.enviromentComplain,
      waterComplain: this.waterComplain,
      mineralComplain: this.mineralComplain,
      landDenounce: this.landDenounce,
      enviromentDenounce: this.enviromentDenounce,
      waterDenounce: this.waterDenounce,
      mineralDenounce: this.mineralDenounce,
      congKhai: this.hasLoggedIn ? null : true,
    } as GetSummaryListDto;
    this.summaryService
      .getMap(this.filter)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: SummaryDto[]) => {
          this.dataMap = response;
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
  }

  toggleMenuLeft() {
    this.visibleFilterLeff = !this.visibleFilterLeff;
    if (!this.visibleFilterLeff) {
      this.hideColumnState = 'hidden';
      this.expandColumnState = 'expanded';
    } else {
      this.hideColumnState = 'visible';
      this.expandColumnState = 'normal';
    }
  }

  private toggleBlockUI(enabled: boolean) {
    if (enabled == true) {
      this.blockedPanel = true;
    } else {
      setTimeout(() => {
        this.blockedPanel = false;
      }, 300);
    }
  }

  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
