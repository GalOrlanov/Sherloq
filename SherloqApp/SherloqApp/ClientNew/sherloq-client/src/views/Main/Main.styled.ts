import styled from "styled-components";

export const MainContainer = styled.div`
  display: grid;
  height: 100vh;
  grid-template-areas:
    "side-menu top-menu"
    "side-menu content-container";
  grid-template-columns: 190px 1fr;
  grid-template-rows: 90px 1fr;
`;

export const SideMenuContainer = styled.div`
  grid-area: side-menu;
  position: relative;
`;
export const TopBarContainer = styled.div`
  grid-area: top-menu;
`;
export const ContentContainer = styled.div`
  grid-area: content-container;
`;
