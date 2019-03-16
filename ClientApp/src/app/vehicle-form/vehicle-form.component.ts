import { Component, OnInit } from '@angular/core';/******************************************************************************************************************* */
import { VehicleService } from '../services/vehicle.service';
import { FeatureService } from '../services/feature.service';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
  makes: any[];
  models: any[];
  vehicle: any = {};
  features: any[];
  constructor(private vehicleservice : VehicleService) { }

  ngOnInit() {
    this.vehicleservice.getMakes().subscribe(makes => {this.makes = makes
      //console.log("MAKES",this.makes);
    });
    
    this.vehicleservice.getFeatures().subscribe(features => {this.features = features
      console.log("FEAUTRES",this.features);
    });
    
  }

  onMakeChange()
  {
    var selectedMake = this.makes.find(m => m.id == this.vehicle.make);
    this.models = selectedMake.models;
    console.log("VEHCILE", this.vehicle);
    
  }
}
