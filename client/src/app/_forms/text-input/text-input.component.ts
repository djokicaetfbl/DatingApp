import { Component, Input, OnInit, Optional, Self } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.css'],
})
export class TextInputComponent implements ControlValueAccessor {
  @Input() label!: string;
  @Input() type = 'text';

  constructor(@Optional() @Self() public ngControl: NgControl) {
    //@Self dekoratorom angular ce da injektuje ono sto mu proslijedimo @ obezbjedjuje null ako ne pronadje vrijednost
    this.ngControl.valueAccessor = this;
  }

  writeValue(obj: any): void {}
  registerOnChange(fn: any): void {}
  registerOnTouched(fn: any): void {}
}
