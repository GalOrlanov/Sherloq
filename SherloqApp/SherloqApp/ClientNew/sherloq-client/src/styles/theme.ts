import { DefaultTheme } from "styled-components";

const theme: DefaultTheme = {
  colors: {
    primary: "#E8EBF7",
    secondary: "#FFFCBB",
    tertiary: "#0B2A97",
  },
  inputsColors: {
    background: "#EDF0F9",
    border: "#AFC0F9",
  },
  statusColors: {
    danger: "#FF5852",
    warning: "#FFD500",
    ok: "#62BB47",
  },
  typography: {
    size: { sm: "12px", md: "16px", lg: "25px", xl: "35px" },
    colors: { primary: "#134BA3", secondary: "#031277", tertiary: "black" },
    bold: { sm: "400", md: "500", lg: "800" },
  },
  spacing: {
    xs: "6px",
    sm: "12px",
    md: "18px",
    lg: "25px",
  },
  borderColor: "rgba(0, 0, 0, 0.5)",
  borderRadius: {
    sm: "8px",
    md: "12px",
    lg: "18px",
  },
  breakpoints: {
    xs: "600px",
    sm: "800px",
    md: "1000px",
    lg: "1200px",
    xl: "1400px",
  },
};

export default theme;
