import styled from "styled-components";

interface AccordionContainerProps {
  expand?: boolean;
}
export const AccordionContainer = styled.div<AccordionContainerProps>`
  border-bottom: 1px solid rgba(0, 0, 0, 0.1);
  padding: ${({ theme }) => theme.spacing.sm};
  display: flex;
  flex-direction: row;
  align-items: center;
  cursor: pointer;
  span {
    user-select: none;
  }
`;

export const ChevronLeft = styled.img<AccordionContainerProps>`
  transition: 200ms;
  transform: ${({ expand }) => (expand ? "rotate(90deg)" : "rotate(0deg)") + "scale(0.6)"};
`;

export const AccordionIcon = styled.img`
  transform: scale(0.6);
`;

export const AccordionContent = styled.div<AccordionContainerProps>`
  height: ${({ expand }) => (expand ? "200px" : "0px")};
  visibility: ${({ expand }) => (expand ? "none" : "hidden")};
  opacity: ${({ expand }) => (expand ? 1 : 0)};

  width: 100%;
  transition: 0.3s;
`;
