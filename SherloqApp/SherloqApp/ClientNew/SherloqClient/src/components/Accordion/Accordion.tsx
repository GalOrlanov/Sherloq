import { AppTypography } from "@/styles/global";
import globalImages from "@/assets/images/global";
import { AccordionContainer, AccordionContent, AccordionIcon, ChevronLeft } from "./Accordion.styled";
import { ReactElement, useState } from "react";
import { TypographyColor } from "@/types/styles.d";

interface AccordionProps {
  icon: string;
  title: string;
  component?: ReactElement;
}

const Accordion: React.FC<AccordionProps> = ({ icon, component, title }) => {
  const [expand, setExpand] = useState<boolean>(false);

  const handleClick = () => setExpand((isExpand) => !isExpand);

  return (
    <>
      <AccordionContainer expand={expand} onClick={handleClick}>
        <ChevronLeft expand={expand} src={globalImages.chevronLeft} alt='' />
        <AccordionIcon alt='' src={icon} />
        <AppTypography textColor={TypographyColor.tertiary}>{title}</AppTypography>
      </AccordionContainer>
      <AccordionContent expand={expand}>{component}</AccordionContent>
    </>
  );
};

export default Accordion;
