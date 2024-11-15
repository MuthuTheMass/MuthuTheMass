import { IMAGE_CONFIG } from '@angular/common';

export const imageConfig = {
  provide: IMAGE_CONFIG,
  useValue: {
    disableImageSizeWarning: true,
    disableImageLazyLoadWarning: true
  }
};
