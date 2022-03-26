import React from "react";
import { AppTypography } from "../../../styles/global";
import { TypographyBold, TypographySize } from "../../../types/styles.d";
import { SelectedLine, SideMenuItemContainer, SideMenuItemIcon } from "./SideMenuItem.styled";

interface SideMenuItemProps {
  icon: string;
  label: string;
  path: string;
  selected?: boolean;
}

const SideMenuItem: React.FC<SideMenuItemProps> = ({ icon, label, path, selected }) => {
  const bold = selected ? TypographyBold.lg : TypographyBold.sm;
  return (
    <SideMenuItemContainer selected={selected} to={path}>
      <SideMenuItemIcon alt={label} src={icon} />
      <AppTypography size={TypographySize.sm} bold={bold}>
        {label}
      </AppTypography>
      <SelectedLine selected={selected} />
    </SideMenuItemContainer>
  );
};

export default SideMenuItem;
