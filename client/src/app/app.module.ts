import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({ // NgModule definise komponente koje su dostupne u tom modulu, a moguce je uvesti i druge module
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule, // da bi nasa SPA aplikacija bila prikayana u veb pregledacu
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent] // butstrapovanje AppComponent
})
export class AppModule { }
