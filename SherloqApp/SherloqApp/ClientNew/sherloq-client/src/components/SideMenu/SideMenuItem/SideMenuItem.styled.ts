import styled from "styled-components";
import { Link } from "react-router-dom";

interface SideMenuItemContainerProps {
  selected?: boolean;
}
interface SelectedLineProps {
  selected?: boolean;
}

export const SideMenuItemContainer = styled(Link)<SideMenuItemContainerProps>`
  gap: ${({ theme }) => theme.spacing.sm};
  background-color: ${({ theme, selected }) => (selected ? theme.colors.primary : "transparen")};
  border-top-left-radius: ${({ theme }) => theme.borderRadius.lg};
  border-bottom-left-radius: ${({ theme }) => theme.borderRadius.lg};
  height: 45px;
  width: 98%;
  margin-left: 3%;
  display: flex;
  flex-direction: row;
  text-decoration: none;
  align-items: center;
`;

export const SideMenuItemIcon = styled.img`
  transform: scale(0.8);
  padding-left: ${({ theme }) => theme.spacing.sm}; ;
`;

export const SelectedLine = styled.div<SelectedLineProps>`
  background-color: ${({ theme, selected }) => (selected ? theme.colors.tertiary : "transparent")};
  border-radius: ${({ theme }) => theme.borderRadius.lg};
  width: 5px;
  margin-left: auto;
  height: 100%;
`;
