import { ListResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { DocumentTypeLookupDto, DocumentTypeService } from '@proxy/document-types';
import { FileAttachmentDto } from '@proxy/file-attachments';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-file-attachment',
  templateUrl: './file-attachment.component.html',
  styleUrls: ['./file-attachment.component.scss'],
})
export class FileAttachmentComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  blockedPanel = false;
  items: FileAttachmentDto[];

  documentTypeOptions: DocumentTypeLookupDto[];
  giaiDoanOptions = [
    { value: 0, text: 'Tất cả' },
    { value: 1, text: 'Khiếu nại lần I' },
    { value: 2, text: 'Khiếu nại lần II' },
  ];

  giaiDoan: number;
  hinhThuc: string;


  constructor(private documentTypeService: DocumentTypeService) {}

  ngOnInit() {
    this.getOptions();
  }
  getOptions() {
    this.toggleBlockUI(true);
    this.documentTypeService
      .getLookup()
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        (res: ListResultDto<DocumentTypeLookupDto>) => {
          this.documentTypeOptions = res.items;
          this.toggleBlockUI(false);
        },
        () => {
          this.toggleBlockUI(false);
        }
      );
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
