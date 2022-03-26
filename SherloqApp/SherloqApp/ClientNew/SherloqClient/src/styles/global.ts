import styled from "styled-components";
import { AppTypographyProps } from "../types/styles";

export const AppTypography = styled.span<AppTypographyProps>`
  font-size: ${({ size, theme }) => size ?? theme.typography.size.sm};
  font-weight: ${({ bold, theme }) => bold ?? theme.typography.bold.sm};
  color: ${({ theme, textColor }) => textColor ?? theme.typography.colors.primary};
  white-space: ${({ wrap }) => (wrap ? "normal" : "nowrap")};
`;
