import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable()
export class AppService {
    constructor(private http: HttpClient) {
    }

    procesarArchivo(file: File, numDocumento: number){
      let formData = new FormData();
      formData.append('ArchivoSeleccionado', file);
      return this.http.post(environment.apiUrl + "Moving/ProcesarArchivo/"+ numDocumento, formData, {responseType : "arraybuffer"})
    }

}
