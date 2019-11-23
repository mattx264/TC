import { Component, OnInit } from '@angular/core';
import { WebRtcService } from '../services/web-rtc.service';

@Component({
  selector: 'app-web-rtc',
  templateUrl: './web-rtc.component.html',
  styleUrls: ['./web-rtc.component.scss']
})
export class WebRtcComponent implements OnInit {

  constructor(private webRtcService: WebRtcService) { }

  ngOnInit() {
  }

}
