import templateUrl from './theme-settings.component.html';
import './theme-settings.component.scss';

class ThemeSettingsController {
  $onInit() {
    this.themes = window.JobPortal.themes;
    this.themeLinkElement = document.getElementById("theme");
    this.selectedThemeId = '';
    for (let i = 0; i < this.themes.length; i++) {
      const theme = this.themes[i];
      if (theme.IsDefault) {
        this.selectedThemeId = theme.ThemeId;
      }
    }
  }

  changeTheme(theme) {
    this.selectedThemeId = theme.ThemeId;
    this.themeLinkElement.setAttribute("href", theme.StylesheetUrl);
  }
}

export const ThemeSettingsComponent = {
  templateUrl,
  controller: ThemeSettingsController
};
