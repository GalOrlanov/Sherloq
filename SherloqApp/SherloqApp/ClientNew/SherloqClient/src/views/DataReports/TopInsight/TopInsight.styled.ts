import styled from "styled-components";

export const TopInsightContainer = styled.div`
  gap: ${({ theme }) => theme.spacing.lg};
  padding: ${({ theme }) => theme.spacing.lg};

  display: flex;
  flex-direction: row;
  align-items: center;
`;
