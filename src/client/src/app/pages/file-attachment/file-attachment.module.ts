import { NgModule } from '@angular/core';
import { PanelModule } from 'primeng/panel';
import { TableModule } from 'primeng/table';
import { PaginatorModule } from 'primeng/paginator';
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { EditorModule } from 'primeng/editor';
import { ImageModule } from 'primeng/image';
import { CalendarModule } from 'primeng/calendar';
import { ToolbarModule } from 'primeng/toolbar';
import { TooltipModule } from 'primeng/tooltip';
import { TieredMenuModule } from 'primeng/tieredmenu';
import { CoreModule } from '@abp/ng.core';
import { CardModule } from 'primeng/card';
import { FileUploadModule } from 'primeng/fileupload';
import { DialogModule } from 'primeng/dialog';
import { SelectButtonModule } from 'primeng/selectbutton';
import { FileAttachmentComponent } from './file-attachment.component';
import { FileAttachmentDetailComponent } from './detial/file-attachment-detail.component';
import { ValidationMessagedModule } from 'src/app/_shared/modules/validation-message/validation-message.module';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { BlockUIModule } from 'primeng/blockui';
import { InputSwitchModule } from 'primeng/inputswitch';
@NgModule({
  declarations: [FileAttachmentComponent, FileAttachmentDetailComponent],
  imports: [
    CoreModule,
    PanelModule,
    TableModule,
    PaginatorModule,
    ButtonModule,
    DropdownModule,
    InputTextModule,
    EditorModule,
    ValidationMessagedModule,
    ImageModule,
    CalendarModule,
    ToolbarModule,
    TooltipModule,
    TieredMenuModule,
    CardModule,
    FileUploadModule,
    SelectButtonModule,
    DialogModule,
    ProgressSpinnerModule,
    BlockUIModule,
    InputSwitchModule
  ],
  exports: [FileAttachmentComponent, FileAttachmentDetailComponent],
})
export class FileAttachmentModule {}
