import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
    ToastrModule.forRoot({ // za ljepse notifikacije za errore :D
      positionClass: 'toast-bottom-right'
    })
  ],

  exports: [ // da su vidljivo u modulima koji importuju SharedModule
    BsDropdownModule,
    ToastrModule
  ]
})
export class SharedModule { }
