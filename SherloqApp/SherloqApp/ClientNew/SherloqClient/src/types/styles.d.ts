import "styled-components";
import theme from "../styles/theme";

interface IPalette {
  main: string;
  contrastText: string;
}
declare module "styled-components" {
  interface DefaultTheme {
    colors: {
      primary: string;
      secondary: string;
      tertiary: string;
    };
    statusColors: {
      danger: string;
      warning: string;
      ok: string;
    };
    typography: {
      size: { sm: string; md: string; lg: string; xl: string };
      colors: { primary: string; secondary: string; tertiary: string };
      bold: { sm: string; md: string; lg: string };
    };
    inputsColors: {
      background: string;
      border: string;
    };
    spacing: {
      xs: string;
      sm: string;
      md: string;
      lg: string;
    };
    borderRadius: {
      sm: string;
      md: string;
      lg: string;
    };
    borderColor: string;
    breakpoints: {
      xs: string;
      sm: string;
      md: string;
      lg: string;
      xl: string;
    };
  }
}

export enum TypographySize {
  sm = theme.typography.size.sm,
  md = theme.typography.size.md,
  lg = theme.typography.size.lg,
}

export enum TypographyColor {
  primary = theme.typography.colors.primary,
  secondary = theme.typography.colors.secondary,
  tertiary = theme.typography.colors.tertiary,
  danger = theme.statusColors.danger,
  ok = theme.statusColors.ok,
  warning = theme.statusColors.warning,
}

interface AppTypographyProps {
  bold?: TypographyBold;
  size?: TypographySize;
  textColor?: TypographyColor | string;
  wrap?: boolean;
}
export enum TypographyBold {
  sm = theme.typography.bold.sm,
  md = theme.typography.bold.md,
  lg = theme.typography.bold.lg,
}
