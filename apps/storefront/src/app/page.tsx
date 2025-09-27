import { HeroSection } from '../components/home/HeroSection';
import { CategoriesSection } from '../components/home/CategoriesSection';
import { FeaturedProducts } from '../components/home/FeaturedProducts';
import { FeaturesSection } from '../components/home/FeaturesSection';
import { NewsletterSection } from '../components/home/NewsletterSection';

export default function Home() {
  return (
    <>
      <HeroSection />
      <CategoriesSection />
      <FeaturedProducts />
      <FeaturesSection />
      <NewsletterSection />
    </>
  );
}