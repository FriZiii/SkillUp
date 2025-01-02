import {
  AfterViewInit,
  Component,
  ElementRef,
  input,
  OnDestroy,
  OnInit,
  ViewChild,
  ViewEncapsulation,
} from '@angular/core';
import videojs from 'video.js';

@Component({
  selector: 'app-videojs',
  templateUrl: './videojs.component.html',
  standalone: true,
  styleUrls: ['./videojs.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class VjsPlayerComponent implements OnDestroy, OnInit {
  @ViewChild('target', { static: true }) target!: ElementRef;

  videoLink = input.required<string>();

  player!: any;
  qualityLevels: any;

  ngOnInit() {
    const options = {
      autoplay: false,
      fluid: true,
      sources: {
        src: this.videoLink(),
        type: 'video/mp4',
      },
      notSupportedMessage: 'Invalid Video URL', // to change the default message
    };

    this.player = videojs(
      this.target.nativeElement,
      options,
      function onPlayerReady() {
        console.log('onPlayerReady', this);
      }
    );
  }

  ngOnDestroy() {
    if (this.player) {
      this.player.dispose();
    }
  }
}
