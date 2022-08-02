export interface IDataCompania {
  isExitoso: boolean;
  resultado: Compania[];
  mensaje: string;
}

export interface Compania {
  id: number;
  nombre: string;
  direccion: string;
  telefono: number;
  telefono2: number;
}
