import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LocationService {

  constructor(
    private http: HttpClient
  ) { }


  getCurrentLocation(): Promise<GeolocationPosition> {
    return new Promise((resolve, reject) => {
      if (!navigator.geolocation) {
        return reject(new Error("Geolocation not supported."));
      }

      navigator.geolocation.getCurrentPosition(resolve, reject, {
        enableHighAccuracy: true,
        timeout: 5000,
        maximumAge: 0
      });
    });
  }

  getIPLocation(): Promise<{ latitude: number; longitude: number }> {
    return this.http
      .get<{ lat: number; lon: number }>('http://ip-api.com/json/')
      .toPromise()
      .then((data) => {
        if (!data || !data.lat || !data.lon) {
          throw new Error('Invalid IP location data');
        }
        return {
          latitude: data.lat,
          longitude: data.lon,
        };
      })
      .catch((error) => {
        throw new Error('Error fetching IP location: ' + error.message);
      });
  }
}
