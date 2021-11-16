import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ProjectDetails } from '../shared/project-details.model';
import { ProjectDetailsService } from '../shared/project-details.service';

@Component({
  selector: 'app-project-details',
  templateUrl: './project-details.component.html',
  styles: [
  ]
})
export class ProjectDetailsComponent implements OnInit {

  constructor(public service: ProjectDetailsService,
    private toastr:ToastrService) { }

  ngOnInit(): void
  {
    this.service.getAllProjects();
  }

  populateForm(selectedRecord:ProjectDetails)
  {
    this.service.formData = Object.assign({},selectedRecord);
  }

  deleteProject(id:string)
  {
    if(confirm('Do you want to delete this record?'))
    {
      this.service.deleteProject(id)
      .subscribe(
        res => 
        {
          this.service.getAllProjects();
          this.toastr.error('Project Deleted Successfully', 'Project Register');
        },
        err => {console.log(err)}
    );
    }
  }
}
