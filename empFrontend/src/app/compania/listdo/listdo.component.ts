import { Component, OnInit } from '@angular/core';
import { CompaniaService } from '../compania.service';

@Component({
  selector: 'app-listdo',
  templateUrl: './listdo.component.html',
  styleUrls: ['./listdo.component.css'],
})
export class ListdoComponent implements OnInit {
  constructor(private companiaService: CompaniaService) {}

  ngOnInit(): void {
    this.companiaService.listarCompanias();
  }

  get resultados(){
    return this.companiaService.resultados;
  }
}
