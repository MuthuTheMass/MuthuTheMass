import {
  AfterContentInit,
  Component,
  ContentChildren,
  QueryList,
  TemplateRef,
  ViewChild,
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
  @ViewChild('carousel') carousel!: NgbCarousel;

  slideTemplates: TemplateRef<any>[] = [];
  currentSlide = 0;

  ngAfterContentInit() {
    this.slideTemplates = this.templates.toArray();
  }

  selectSlide(index: number) {
    this.carousel.select(`ngb-slide-${index}`);
    this.currentSlide = index;
  }
}
