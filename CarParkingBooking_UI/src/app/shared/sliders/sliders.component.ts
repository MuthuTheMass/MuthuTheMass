import {
  AfterContentInit,
  Component,
  ContentChildren,
  QueryList,
  TemplateRef,
} from '@angular/core';
import { NgbCarousel, NgbSlide } from '@ng-bootstrap/ng-bootstrap';
import { CommonModule, NgTemplateOutlet } from '@angular/common';

@Component({
  selector: 'app-sliders',
  imports: [NgbCarousel, NgTemplateOutlet, NgbSlide, CommonModule],
  templateUrl: './sliders.component.html',
  styleUrl: './sliders.component.css',
})
export class SlidersComponent implements AfterContentInit {
  @ContentChildren(TemplateRef) templates!: QueryList<TemplateRef<any>>;
  slideTemplates: TemplateRef<any>[] = [];

  ngAfterContentInit(): void {
    this.slideTemplates = this.templates.toArray();
  }
}
