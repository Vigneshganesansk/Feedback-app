import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';


@Injectable({
    providedIn: 'root'
  })
export class Service {
    private configSelect = new BehaviorSubject<string>('isPMO');

    configMode = this.configSelect.asObservable();
    
    constructor(){
        
    }
    setConfigMode(mode: string){
        this.configSelect.next(mode);
    }
}
