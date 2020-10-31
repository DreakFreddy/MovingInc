import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AppService } from './app.service';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [AppService]
})
export class AppComponent  implements OnInit{
  registerForm: FormGroup;
  submitted = false;
  file: File;
  numDocumento: number = null;

  constructor(private formBuilder: FormBuilder, private appService: AppService) { }

  ngOnInit() {
    // inicializar los campos del formulario con sus respectivas validaciones
    this.registerForm = this.formBuilder.group({
      numDocumento: ['', Validators.required],
      archivo: ['', Validators.required]
    });
  }

  // para acceder facil a los campos
  get f() { return this.registerForm.controls; }

  downLoadFile(data: any, type: string) {
      var blob = new Blob([data], { type: type.toString() });
      var url = window.URL.createObjectURL(blob);
      saveAs(blob,"ArchivoSalida.txt");
  }

  procesarArchivo() {
      this.submitted = true;

      // validar los capos del formulario
      if (this.registerForm.invalid) {
          return;
      }  
    
      this.appService.procesarArchivo(this.file, this.numDocumento).subscribe(Response => {
        this.downLoadFile(Response,"text/plain")
      });

  }

  cargarArchivo(evento: any) {
    this.file = evento.target.files[0];
    if (!this.validateFile(this.file.name)) {
      alert('El tipo de documento es inválido'); 
      return;      
  }
}  
  validateFile(name: String) {
    var ext = name.substring(name.lastIndexOf('.') + 1);
    if (ext.toLowerCase() == 'txt') {
        return true;
    }
    else {
        return false;
    }
}

}
