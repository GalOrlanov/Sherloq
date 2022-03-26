import styled from "styled-components";
import theme from "../../styles/theme";

export const RectDataContainer = styled.div`
  border: 1px solid ${({ theme }) => theme.borderColor};
  border-radius: ${({ theme }) => theme.borderRadius.lg};
  gap: ${({ theme }) => theme.spacing.md};
  display: flex;
  flex-direction: column;
  justify-content: center;
  text-align: center;
  align-items: center;
  width: 130px;
  height: 140px;
`;
