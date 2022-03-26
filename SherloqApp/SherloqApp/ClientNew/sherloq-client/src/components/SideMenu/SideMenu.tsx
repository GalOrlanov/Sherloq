import React from "react";
import routes from "../../views/Main/routes";
import { SideMenuContainer, SideMenuLogo } from "./SideMenu.styled";
import SideMenuItem from "./SideMenuItem/SideMenuItem";
import globalImages from "../../assets/images/global";
interface SideMenuProps {
  selectedRoute: string;
}
const SideMenu: React.FC<SideMenuProps> = ({ selectedRoute }) => {
  return (
    <SideMenuContainer>
      <SideMenuLogo src={globalImages.logo} alt='' />
      {routes.map(({ icon, title, path }) => {
        return <SideMenuItem selected={selectedRoute === path} key={title} icon={icon} path={path} label={title} />;
      })}
    </SideMenuContainer>
  );
};

export default SideMenu;
