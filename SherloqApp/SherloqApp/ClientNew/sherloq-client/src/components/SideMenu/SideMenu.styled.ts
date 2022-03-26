import styled from "styled-components";

export const SideMenuContainer = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: center;
  position: relative;
  height: 100%;
  box-shadow: 18px 4px 35px rgba(0, 0, 0, 0.02);
`;

export const SideMenuLogo = styled.img`
  margin-bottom: ${({ theme }) => theme.spacing.lg};
  width: 122px;
`;
