import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';
import { TabsModule } from 'ngx-bootstrap/tabs'; // da iuvedemo tabove iz ngx-bootstrap
import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import { FileUploadModule } from 'ng2-file-upload';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
    ToastrModule.forRoot({
      // za ljepse notifikacije za errore :D
      positionClass: 'toast-bottom-right',
    }),
    TabsModule.forRoot(), // uveden je tabs module iz ngx-bootstrap : https://valor-software.com/ngx-bootstrap/#/documentation
    NgxGalleryModule,
    FileUploadModule,
  ],

  exports: [
    // da su vidljivo u modulima koji importuju SharedModule
    BsDropdownModule,
    ToastrModule,
    TabsModule,
    NgxGalleryModule,
    FileUploadModule,
  ],
})
export class SharedModule {}
