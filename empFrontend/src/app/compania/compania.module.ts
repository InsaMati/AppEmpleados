import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListdoComponent } from './listdo/listdo.component';
import { AgregarComponent } from './agregar/agregar.component';
import { EditarComponent } from './editar/editar.component';
import { EliminarComponent } from './eliminar/eliminar.component';
import { HomeComponent } from './home/home.component';
import { CompaniaRoutingModule } from './compania-routing.module';

@NgModule({
  declarations: [
    ListdoComponent,
    AgregarComponent,
    EditarComponent,
    EliminarComponent,
    HomeComponent,
  ],
  imports: [CommonModule,CompaniaRoutingModule],
})
export class CompaniaModule {}
