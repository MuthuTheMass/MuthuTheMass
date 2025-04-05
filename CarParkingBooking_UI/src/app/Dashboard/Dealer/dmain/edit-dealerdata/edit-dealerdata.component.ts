import { Component } from '@angular/core';

@Component({
  selector: 'app-edit-dealerdata',
  standalone: true,
  imports: [],
  templateUrl: './edit-dealerdata.component.html',
  styleUrl: './edit-dealerdata.component.css'
})
export class EditDealerdataComponent {
  ngOnInit(): void {
    let current_fs: HTMLElement | null;
    let next_fs: HTMLElement | null;
    let previous_fs: HTMLElement | null;
    let opacity: number;
    let current = 1;

    const fieldsets = document.querySelectorAll<HTMLFieldSetElement>("fieldset");
    const steps = fieldsets.length;

    setProgressBar(current);

    document.querySelectorAll<HTMLElement>(".next").forEach(button => {
      button.addEventListener("click", () => {
        current_fs = button.parentElement;
        next_fs = current_fs?.nextElementSibling as HTMLElement;

        if (!next_fs) return;

        const index = Array.from(fieldsets).indexOf(next_fs as HTMLFieldSetElement);
        const progressItems = document.querySelectorAll<HTMLLIElement>("#progressbar li");
        progressItems[index].classList.add("active");

        next_fs.style.display = "block";

        animateOpacity(current_fs, next_fs, true);
        setProgressBar(++current);
      });
    });

    document.querySelectorAll<HTMLElement>(".previous").forEach(button => {
      button.addEventListener("click", () => {
        current_fs = button.parentElement;
        previous_fs = current_fs?.previousElementSibling as HTMLElement;

        if (!previous_fs) return;

        const index = Array.from(fieldsets).indexOf(current_fs as HTMLFieldSetElement);
        const progressItems = document.querySelectorAll<HTMLLIElement>("#progressbar li");
        progressItems[index].classList.remove("active");

        previous_fs.style.display = "block";

        animateOpacity(current_fs, previous_fs, true);
        setProgressBar(--current);
      });
    });

    function animateOpacity(from: HTMLElement | null, to: HTMLElement | null, forward: boolean) {
      if (!from || !to) return;

      let start = 0;
      const duration = 500;
      const stepTime = 10;
      const steps = duration / stepTime;

      const stepFunc = () => {
        start++;
        const progress = start / steps;
        opacity = forward ? progress : 1 - progress;

        from.style.opacity = (1 - progress).toString();
        from.style.display = "none";
        from.style.position = "relative";

        to.style.opacity = opacity.toString();

        if (start < steps) {
          requestAnimationFrame(stepFunc);
        }
      };
      requestAnimationFrame(stepFunc);
    }

    function setProgressBar(curStep: number): void {
      const percent = (100 / steps) * curStep;
      const rounded = Math.round(percent);
      const progressBar = document.querySelector<HTMLElement>(".progress-bar");
      if (progressBar) {
        progressBar.style.width = `${rounded}%`;
      }
    }

    document.querySelectorAll<HTMLElement>(".submit").forEach(button => {
      button.addEventListener("click", (e) => {
        e.preventDefault(); // Prevent form submission
      });
    });
  }
}


