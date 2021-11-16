import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ProjectDetailsService } from 'src/app/shared/project-details.service';

@Component({
  selector: 'app-project-details-form',
  templateUrl: './project-details-form.component.html',
  styles: [
  ]
})
export class ProjectDetailsFormComponent implements OnInit {

  constructor(public service:ProjectDetailsService) { }

  ngOnInit(): void {
  }

  onSubmit(form:NgForm){
    this.service.postProjectDetails().subscribe(
      res =>{

      },
      err =>{ console.log(err);}
    );
  }

}
