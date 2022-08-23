import { Component, OnInit } from '@angular/core';
import { CompaniaService } from '../compania.service';

@Component({
  selector: 'app-listdo',
  templateUrl: './listdo.component.html',
  styleUrls: ['./listdo.component.css'],
})
export class ListdoComponent implements OnInit {
  
  displayedColumns: string[] = [
    'id',
    'nombre',
    'direccion',
    'telefono',
    'telefono2',
  ];

  constructor(private companiaService: CompaniaService) {}

  ngOnInit(): void {
    this.companiaService.listarCompanias();
  }

  get resultado() {
    return this.companiaService.resultados;
  }
}
