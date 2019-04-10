import { Component, OnInit } from '@angular/core';/******************************************************************************************************************* */
import { VehicleService } from '../services/vehicle.service';
import { FeatureService } from '../services/feature.service';
import { ToastyService } from 'ng2-toasty';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/forkJoin';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
  makes: any[];
  models: any[];
  vehicle: any = {
    features: [],
    contact: {}
  };
  features: any[];
  constructor(
              private route: ActivatedRoute,
              private router: Router,
              private vehicleservice : VehicleService, 
              private toastyService : ToastyService) {

                  route.params.subscribe(p => {
                     this.vehicle.id = +p["id"]; 
                  });
               }

  ngOnInit() {

    Observable.forkJoin(
     [
        this.vehicleservice.getMakes(),
        this.vehicleservice.getFeatures(),
        this.vehicleservice.getVehicle(this.vehicle.id)
     ]
    ).subscribe(data => {
        this.makes = data[0],
        this.features = data[1],
        this.vehicle = data[2]
    })
    // , err => {
    //   if(err.status == 404)
    //   {
    //     this.router.navigate(['/home']);
    //   }
    // });


    // this.vehicleservice.getVehicle(this.vehicle.id)
    //     .subscribe(v => {
    //       this.vehicle = v;    
    //     });

    // this.vehicleservice.getMakes().subscribe(makes => {this.makes = makes
    //   //console.log("MAKES",this.makes);
    // });
    
    // this.vehicleservice.getFeatures().subscribe(features => {this.features = features
    //   console.log("FEAUTRES",this.features);
    // });
    
  }

  onMakeChange()
  {
    var selectedMake = this.makes.find(m => m.id == this.vehicle.makeId);
    this.models = selectedMake.models;
    delete this.vehicle.modelId;
    console.log("VEHCILE", this.vehicle);
    
  }

  onFeatureToggle(featureId, $event)
  {
    if ($event.target.checked)
      this.vehicle.features.push(featureId);
    else
    {
       var index = this.vehicle.features.indexOf(featureId);
       this.vehicle.features.splice(index, 1);
    }
  }

  submit()
  {
    this.vehicleservice.saveVehicle(this.vehicle)
                       .subscribe(x => console.log(x),
                                  // err => {
                                    
                                    // }
                                  );
  }
}
