import RectData from "@/components/RectData/RectData";
import { topInsights } from "@/utils/consts/dataReports";
import { TopInsightContainer } from "./TopInsight.styled";

const TopInsight = () => {
  return (
    <TopInsightContainer>
      {topInsights.map(({ titleColor, title, value }) => {
        return <RectData key={title} valueColor={titleColor} title={title} value={value} />;
      })}
    </TopInsightContainer>
  );
};

export default TopInsight;
