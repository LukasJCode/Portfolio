import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ProjectDetails } from 'src/app/shared/project-details.model';
import { ProjectDetailsService } from 'src/app/shared/project-details.service';

@Component({
  selector: 'app-project-details-form',
  templateUrl: './project-details-form.component.html',
  styles: [
  ]
})
export class ProjectDetailsFormComponent implements OnInit {

  constructor(public service:ProjectDetailsService, private toastr:ToastrService) { }

  ngOnInit(): void {
  }

  onSubmit(form:NgForm){
    if(this.service.formData.id == '')
    {
      this.insertRecord(form);
    }
    else
    {
      this.updateRecord(form);
    }
      
  }

  insertRecord(form:NgForm)
  {
    this.service.postProjectDetails().subscribe(
      res =>{
        this.resetForm(form);
        this.toastr.success('Project Created Successfully', 'Project Register');
        this.service.getAllProjects();
      },
      err =>{ console.log(err);}
    );
  }

  updateRecord(form:NgForm)
  {
    this.service.updateProject().subscribe(
      res =>{
        this.resetForm(form);
        this.toastr.info('Project Updated Successfully', 'Project Register');
        this.service.getAllProjects();
      },
      err =>{ console.log(err);}
    );
    
  }

  resetForm(form:NgForm)
  {
    form.form.reset();
    this.service.formData = new ProjectDetails();
  }

}
