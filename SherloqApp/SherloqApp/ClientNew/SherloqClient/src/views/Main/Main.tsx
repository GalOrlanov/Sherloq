import React from "react";
import { ContentContainer, MainContainer, SideMenuContainer, TopBarContainer } from "./Main.styled";
import { Route, Routes, useLocation } from "react-router-dom";
import routes from "./routes";
import SideMenu from "@/components/SideMenu/SideMenu";
import DataReports from "../DataReports/Datareports";

const Main = () => {
  const location = useLocation();

  return (
    <MainContainer>
      <SideMenuContainer>
        <SideMenu selectedRoute={location.pathname} />
      </SideMenuContainer>
      <TopBarContainer></TopBarContainer>
      <ContentContainer>
        <Routes location={location}>
          {routes.map(({ path, title, component }) => {
            return <Route key={title} path={path} element={component() as any} />;
          })}
        </Routes>
      </ContentContainer>
    </MainContainer>
  );
};
export default Main;
