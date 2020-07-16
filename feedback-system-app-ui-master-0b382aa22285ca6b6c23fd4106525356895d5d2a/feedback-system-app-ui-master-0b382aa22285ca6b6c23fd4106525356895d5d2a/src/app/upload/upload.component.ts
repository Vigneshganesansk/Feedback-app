import { Component, OnInit, ViewChild } from '@angular/core';
import { Observable } from 'rxjs';
import { StorageService } from '../_services/storage.service';
import { AlertService } from '../_services/alert.service';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.css']
})
export class UploadComponent implements OnInit {
  userRole$: Observable<number>;

  constructor(private storageService: StorageService, private alertService: AlertService) { }
  file: File;

  onSelect(event) {
    console.log(event);
    this.file = event.addedFiles;
  }

  onRemove(event) {
    console.log(event);
    this.file = null;
  }
  upload() {
    this.storageService.postExcelData(this.file).subscribe(
      data=>{
        if(data){
          this.alertService.successAlert("File upload successful");
        }
        else{
          this.alertService.errorAlert("File upload failed!! :(");
          return;
        }
      }
    );
    
  }
  ngOnInit() {
  }

}
