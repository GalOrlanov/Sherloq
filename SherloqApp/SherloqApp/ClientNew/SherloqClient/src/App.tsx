import React from "react";
import { ThemeProvider } from "styled-components";
import { BrowserRouter } from "react-router-dom";
import Main from "./views/Main/Main";
import theme from "./styles/theme";
import { AppContainer } from "./App.styled";

function App() {
  return (
    <AppContainer>
      <ThemeProvider theme={theme}>
        <BrowserRouter>
          <Main />
        </BrowserRouter>
      </ThemeProvider>
    </AppContainer>
  );
}

export default App;
