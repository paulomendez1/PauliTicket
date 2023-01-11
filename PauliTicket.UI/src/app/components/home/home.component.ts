import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatOption } from '@angular/material/core';
import { Router } from '@angular/router';
import { EventService } from 'src/app/services/event.service';
import { CategoryService } from '../../services/home.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  public events!: any[];

  categoryForm!: FormGroup;

  public categories!: any[];
  public categoriesName!: any[];

  @ViewChild('allSelected') private allSelected!: MatOption;

  constructor(private eventService: EventService, private categoryService: CategoryService, private fb: FormBuilder, private router: Router) { }

  public get categoryFormControl(): FormControl {
    return this.categoryForm.get('categoryFormControl') as FormControl;
  }

  ngOnInit(): void {
    this.getCategories();
    this.categoryForm = this.fb.group({
      categoryFormControl: new FormControl('')
    });
  }

  public getEvents(categoriesName: string[]) {
    this.eventService.getEventsForCategories(categoriesName).subscribe(result => {
      this.events = result;
      console.log(this.events);
    })
  }

  public getCategories() {
    this.categoryService.getCategories().subscribe(result => {
      this.categories = result;
      this.categoriesName = this.categories.map(function (a) { return a.name; });
      this.categoryFormControl.setValue(this.categoriesName);
      this.getEvents(this.categoriesName);
    })
  }

  public togglePerOne(): void {
    if (this.allSelected.selected) {
      this.allSelected.deselect();
    }
    if (this.categoryFormControl.value.length == this.categories.length)
      this.allSelected.select();
    this.getEvents(this.categoryFormControl.value);
  }

  public toggleAllSelection(): void {
    if (this.allSelected.selected) {
      this.categoryFormControl
        .patchValue([...this.categories.map(item => item.name), 0]);
    } else {
      this.categoryFormControl.patchValue([]);
    }
    this.getEvents(this.categoryFormControl.value);
  }

  public navigateToOrder(id: string) {
    this.router.navigate([`/event/${id}`])
  }
}