import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.prod';
import { Compania, IDataCompania } from './Interfaces/IDataCompania';

@Injectable({
  providedIn: 'root',
})
export class CompaniaService {
  apiUrl: string = environment.ApiUrl;
  companiaUrl: string = `${this.apiUrl}/compania`;
  resultados: Compania[] = [];

  constructor(private Http: HttpClient) {}

  listarCompanias() {
    this.Http.get<IDataCompania>(this.companiaUrl).subscribe((resp) => {
      this.resultados = resp.resultado;
    });
  }
}
