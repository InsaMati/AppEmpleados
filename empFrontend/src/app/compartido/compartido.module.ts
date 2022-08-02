import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home/home.component';
import { HeaderComponent } from './header/header.component';
import { MaterialModule } from '../material/material.module';

@NgModule({
  declarations: [HomeComponent, HeaderComponent],
  imports: [CommonModule,MaterialModule],
  exports:[HomeComponent,HeaderComponent]
})
export class CompartidoModule {}
