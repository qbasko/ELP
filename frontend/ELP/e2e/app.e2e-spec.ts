import { ELPPage } from './app.po';

describe('elp App', () => {
  let page: ELPPage;

  beforeEach(() => {
    page = new ELPPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
